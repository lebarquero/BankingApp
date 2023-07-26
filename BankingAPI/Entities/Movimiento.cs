using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BankingAPI.Entities.Enums;

namespace BankingAPI.Entities
{
    public class Movimiento
    {
        [Key]
        public int MovimientoID { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public TipoMovimiento TipoMovimiento { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public decimal Saldo { get; set; }

        [ForeignKey(nameof(Cuenta))]
        public string NumeroCuenta { get; set; } = null!;

        public virtual Cuenta Cuenta { get; set; } = null!;
    }
}
