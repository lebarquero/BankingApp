using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BankingAPI.Entities.Enums;

namespace BankingAPI.Entities
{
    public class Cuenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(6)]
        public string NumeroCuenta { get; set; } = null!;

        [Required]
        public TipoCuenta TipoCuenta { get; set; }

        [Required]
        public decimal SaldoInicial { get; set; }

        [Required]
        public bool Estado { get; set; }

        [ForeignKey(nameof(Cliente))]
        public int ClienteID { get; set; }

        public virtual Cliente Cliente { get; set; } = null!;

        public virtual ICollection<Movimiento>? Movimientos { get; set; }
    }
}
