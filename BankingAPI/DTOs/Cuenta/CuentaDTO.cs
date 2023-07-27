using BankingAPI.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs.Cuenta
{
    public class CuentaDTO
    {
        public string NumeroCuenta { get; set; } = null!;

        public string TipoCuenta { get; set; } = null!;

        public decimal SaldoInicial { get; set; }

        public bool Estado { get; set; }

        public int ClienteID { get; set; }
    }
}
