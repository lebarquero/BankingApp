using BankingAPI.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankingAPI.Entities
{
    public abstract class Persona
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El Nombre es requerido")]
        public string Nombre { get; set; } = null!;

        public Genero? Genero { get; set; }

        public int? Edad { get; set; }

        [StringLength(50)]
        public string? Identificacion { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "La Dirección es requerida")]
        public string Direccion { get; set; } = null!;

        [StringLength(9)]
        public string Telefono { get; set; } = null!;
    }
}
