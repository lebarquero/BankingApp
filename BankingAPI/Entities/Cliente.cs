using System.ComponentModel.DataAnnotations;

namespace BankingAPI.Entities
{
    public class Cliente : Persona
    {
        [StringLength(4)]
        [Required(ErrorMessage = "La Contraseña es requerida")]
        public string Contrasena { get; set; } = null!;

        [Required(ErrorMessage = "El Estado es requerido")]
        public bool Estado { get; set; }
    }
}
