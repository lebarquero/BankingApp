using BankingAPI.Business.IServices;
using BankingAPI.DataAccess.Repositories.IRepositories;
using BankingAPI.DTOs.Movimiento;
using BankingAPI.Entities;
using BankingAPI.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankingAPI.Business
{
    public class MovimientoService : IMovimientoService
    {
        private readonly IRepository<Movimiento> _repo;

        public MovimientoService(IRepository<Movimiento> repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(Movimiento entity)
        {
            await _repo.CreateAsync(entity);
        }

        public async Task<IEnumerable<Movimiento>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Movimiento> GetAsync(int id)
        {
            return await _repo.GetAsync(i => i.MovimientoID == id, tracked: false);
        }

        public async Task RemoveAsync(Movimiento entity)
        {
            await _repo.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Movimiento entity)
        {
            await _repo.UpdateAsync(entity);
        }

        public async Task<List<MovimientosPorClienteDTO>> GetMovimientosPorClienteAsync(int clienteId, DateTime startDate, DateTime endDate)
        {
            var result = await _repo.GetAllAsync(i => i.Cuenta.ClienteID == clienteId && i.Fecha >= startDate && i.Fecha <= endDate, i => i.Cuenta.Cliente);
            return result.Select(i => new MovimientosPorClienteDTO
                {
                    Fecha = i.Fecha,
                    Cliente = i.Cuenta.Cliente.Nombre,
                    NumeroCuenta = i.NumeroCuenta,
                    Tipo = i.TipoMovimiento.ToString(),
                    SaldoInicial = 0,
                    Estado = i.Cuenta.Estado,
                    Movimiento = i.Valor,
                    SaldoDisponible = i.Saldo
                }).ToList();
        }
    }
}
