using System.ComponentModel.DataAnnotations;

namespace BankingAPI.Entities
{
    public class Cliente : Persona
    {
        [StringLength(4)]
        public string Contrasena { get; set; } = null!;

        public bool Estado { get; set; }
    }
}
