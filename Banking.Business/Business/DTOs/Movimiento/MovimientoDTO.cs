using BankingAPI.Entities.Enums;

namespace BankingAPI.Business.DTOs.Movimiento
{
    public class MovimientoDTO
    {
        public int MovimientoID { get; set; }

        public DateTime Fecha { get; set; }

        public string TipoMovimiento { get; set; } = null!;

        public decimal Valor { get; set; }

        public decimal Saldo { get; set; }

        public string NumeroCuenta { get; set; } = null!;
    }
}
