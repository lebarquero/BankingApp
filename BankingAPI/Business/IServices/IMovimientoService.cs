using BankingAPI.Business.DTOs.Movimiento;
using BankingAPI.Entities;

namespace BankingAPI.Business.IServices
{
    public interface IMovimientoService : IService<Movimiento>
    {
        Task<List<MovimientosPorClienteDTO>> GetMovimientosPorClienteAsync(int clientId, DateTime startDate, DateTime endDate);
    }
}
