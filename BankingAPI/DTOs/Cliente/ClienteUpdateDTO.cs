using BankingAPI.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs.Cliente
{
    public class ClienteUpdateDTO
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [EnumDataType(typeof(Genero))]
        public string Genero { get; set; } = null!;

        public int? Edad { get; set; }

        [StringLength(50)]
        public string? Identificacion { get; set; }

        [StringLength(50)]
        public string Direccion { get; set; } = null!;

        [StringLength(9)]
        public string Telefono { get; set; } = null!;

        [StringLength(4)]
        public string Contrasena { get; set; } = null!;

        public bool Estado { get; set; }
    }
}
