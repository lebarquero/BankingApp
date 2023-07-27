using BankingAPI.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs.Cuenta
{
    public class CuentaCreateDTO
    {
        [StringLength(6, MinimumLength = 6, ErrorMessage = "La longuitud del número de cuenta debe ser de digitos")]
        [Required(ErrorMessage = "El número de cuenta es requerido")]
        [RegularExpression("^\\d+$", ErrorMessage = "Solo se permiten números")]
        public string NumeroCuenta { get; set; } = null!;

        [Required(ErrorMessage = "El tipo de cuenta es requerido")]
        [EnumDataType(typeof(TipoCuenta), ErrorMessage = "Solo se permiten los siguientes valores: Ahorro, Corriente")]
        public string TipoCuenta { get; set; } = null!;

        [Required(ErrorMessage="El saldo inicial es requerido")]
        public decimal SaldoInicial { get; set; }

        [Required(ErrorMessage="El estado es requerido")]
        public bool Estado { get; set; }

        [Required(ErrorMessage="El cliente es requerido")]
        public int ClienteID { get; set; }
    }
}
