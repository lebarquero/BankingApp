using BankingAPI.Business.DTOs.Movimiento;
using BankingAPI.Business.Exceptions;
using BankingAPI.Business.IServices;
using BankingAPI.DataAccess;
using BankingAPI.DataAccess.Repositories.IRepositories;
using BankingAPI.Entities;
using BankingAPI.Entities.Enums;

namespace BankingAPI.Business
{
    public class MovimientoService : IMovimientoService
    {
        private readonly IRepository<Movimiento> _repo;
        private readonly IRepository<Cuenta> _cuentaRepo;
        private readonly IUnitOfWork _unitOfWork;
        const int LimiteDiarioDeRetiro = 1000;

        public MovimientoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.GetRepository<Movimiento>();
            _cuentaRepo = _unitOfWork.GetRepository<Cuenta>();
        }

        public async Task CreateAsync(Movimiento movimiento)
        {
            Cuenta cuenta = await GetCuentaAsync(movimiento.NumeroCuenta);
            await ValidarMovimientoAsync(movimiento, cuenta);
            UpdateCuenta(movimiento, cuenta);

            movimiento.Saldo = cuenta.SaldoInicial;

            await _cuentaRepo.UpdateAsync(cuenta);
            await _repo.CreateAsync(movimiento);

            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Movimiento>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Movimiento> GetAsync(int id)
        {
            return await _repo.GetAsync(i => i.MovimientoID == id, tracked: false);
        }

        public async Task RemoveAsync(Movimiento movimiento)
        {
            Cuenta cuenta = await GetCuentaAsync(movimiento.NumeroCuenta);
            UpdateCuenta(movimiento, cuenta, revert: true);

            await _cuentaRepo.UpdateAsync(cuenta);
            await _repo.RemoveAsync(movimiento);

            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(Movimiento movimiento)
        {
            Cuenta cuenta = await GetCuentaAsync(movimiento.NumeroCuenta);
            await ValidarMovimientoAsync(movimiento, cuenta);
            UpdateCuenta(movimiento, cuenta);

            movimiento.Saldo = cuenta.SaldoInicial;

            await _cuentaRepo.UpdateAsync(cuenta);
            await _repo.UpdateAsync(movimiento);

            await _unitOfWork.SaveAsync();
        }

        public async Task<List<MovimientosPorClienteDTO>> GetMovimientosPorClienteAsync(int clienteId, DateTime startDate, DateTime endDate)
        {
            var result = await _repo.GetAllAsync(i => i.Cuenta.ClienteID == clienteId && i.Fecha >= startDate && i.Fecha <= endDate, i => i.Cuenta.Cliente);
            return result.Select(i => new MovimientosPorClienteDTO
                {
                    Fecha = i.Fecha,
                    Cliente = i.Cuenta.Cliente.Nombre,
                    NumeroCuenta = i.NumeroCuenta,
                    Tipo = i.Cuenta.TipoCuenta.ToString(),
                    SaldoInicial = i.TipoMovimiento == TipoMovimiento.Debito ? i.Saldo + i.Valor : i.Saldo - i.Valor,
                    Estado = i.Cuenta.Estado,
                    Movimiento = i.TipoMovimiento == TipoMovimiento.Debito ? -i.Valor : i.Valor,
                    SaldoDisponible = i.Saldo
                }).ToList();
        }

        private async Task<Cuenta> GetCuentaAsync(string numeroCuenta) => await _cuentaRepo.GetAsync(c => c.NumeroCuenta == numeroCuenta);

        private void UpdateCuenta(Movimiento movimiento, Cuenta cuenta, bool revert = false)
        {
            if (!revert)
            {
                cuenta.SaldoInicial += movimiento.TipoMovimiento == TipoMovimiento.Debito ? -movimiento.Valor : movimiento.Valor;
            }
            else
            {
                cuenta.SaldoInicial += movimiento.TipoMovimiento == TipoMovimiento.Debito ? movimiento.Valor : -movimiento.Valor;
            }
        }

        private async Task ValidarMovimientoAsync(Movimiento movimiento, Cuenta cuenta)
        {
            if (movimiento.TipoMovimiento == TipoMovimiento.Debito && (cuenta.SaldoInicial - movimiento.Valor) < 0)
                throw new BankingAppException("Saldo no disponible");

            if (await CupoExcedidoAsync(movimiento))
                throw new BankingAppException("Cupo diario excedido");
        }

        private async Task<bool> CupoExcedidoAsync(Movimiento movimiento)
        {
            if (movimiento.TipoMovimiento == TipoMovimiento.Debito)
            {
                var debitosDeHoy = await _repo.GetAllAsync(i => i.TipoMovimiento == TipoMovimiento.Debito &&
                                                               i.NumeroCuenta == movimiento.NumeroCuenta &&
                                                               i.Fecha.Date == DateTime.Today);

                if ((debitosDeHoy.Sum(i => i.Valor) + movimiento.Valor) > LimiteDiarioDeRetiro)
                    return true;
            }

            return false;
        }
    }
}
