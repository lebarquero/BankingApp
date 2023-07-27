using BankingAPI.Entities.Enums;

namespace BankingAPI.DTOs.Cliente
{
    public class ClienteDTO
    {
        public int ID { get; set; }

        public string Nombre { get; set; } = null!;

        public Genero? Genero { get; set; }

        public int? Edad { get; set; }

        public string? Identificacion { get; set; }

        public string Direccion { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string Contrasena { get; set; } = null!;

        public bool Estado { get; set; }
    }
}
