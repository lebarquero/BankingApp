using BankingAPI.Business.DTOs.Movimiento;
using BankingAPI.Business.IServices;
using BankingAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IMovimientoService _service;

        public ReportesController(IMovimientoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{clienteId}")]
        public async Task<ActionResult<IEnumerable<MovimientosPorClienteDTO>>> GetTransactionsByClient(int clienteId, DateTime startDate, DateTime endDate)
        {
            var list = await _service.GetMovimientosPorClienteAsync(clienteId, startDate, endDate);
            return Ok(list);
        }
    }
}
