using BankingAPI.Entities.Enums;

namespace BankingAPI.DTOs.Movimiento
{
    public class MovimientoDTO
    {
        public int MovimientoID { get; set; }

        public DateTime Fecha { get; set; }

        public TipoMovimiento TipoMovimiento { get; set; }

        public decimal Valor { get; set; }

        public decimal Saldo { get; set; }

        public string NumeroCuenta { get; set; } = null!;
    }
}
