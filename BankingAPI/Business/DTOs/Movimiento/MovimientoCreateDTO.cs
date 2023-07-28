using BankingAPI.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankingAPI.Business.DTOs.Movimiento
{
    public class MovimientoCreateDTO
    {
        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime Fecha { get; set; }

        [EnumDataType(typeof(TipoMovimiento), ErrorMessage = "Solo se permiten los siguientes valores: Debito, Credito")]
        [Required(ErrorMessage = "El tipo de movimiento es requerido")]
        public string TipoMovimiento { get; set; } = null!;

        [Required(ErrorMessage = "El valor es requerido")]
        public decimal Valor { get; set; }

        [StringLength(6, MinimumLength = 6, ErrorMessage = "La longuitud del número de cuenta debe ser de digitos")]
        [Required(ErrorMessage = "El número de cuenta es requerido")]
        [RegularExpression("^\\d+$", ErrorMessage = "Solo se permiten números")]
        public string NumeroCuenta { get; set; } = null!;
    }
}
