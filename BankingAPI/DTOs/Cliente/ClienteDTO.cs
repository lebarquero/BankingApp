using BankingAPI.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs.Cliente
{
    public class ClienteDTO
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        public Genero? Genero { get; set; }

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
