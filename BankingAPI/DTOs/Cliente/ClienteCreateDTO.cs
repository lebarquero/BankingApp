using BankingAPI.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs.Cliente
{
    public class ClienteCreateDTO
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50, ErrorMessage = "La longuitud máxima permitida es de 50 caracteres")]
        public string Nombre { get; set; } = null!;

        [EnumDataType(typeof(Genero), ErrorMessage = "Solo se permiten los siguientes valores: Femenino, Masculino")]
        public string Genero { get; set; } = null!;

        public int? Edad { get; set; }

        [StringLength(50, ErrorMessage = "La longuitud máxima permitida es de 50 caracteres")]
        public string? Identificacion { get; set; }

        [StringLength(50, ErrorMessage = "La longuitud máxima permitida es de 50 caracteres")]
        public string Direccion { get; set; } = null!;

        [StringLength(9, MinimumLength = 9, ErrorMessage = "La longuitud debe ser de 9 digitos")]
        [RegularExpression("^\\d+$", ErrorMessage = "Solo se permiten números")]
        public string Telefono { get; set; } = null!;

        [StringLength(4, MinimumLength = 4, ErrorMessage = "La longuitud debe ser de 4 digitos")]
        [RegularExpression("^\\d+$", ErrorMessage = "Solo se permiten números")]
        public string Contrasena { get; set; } = null!;

        public bool Estado { get; set; }
    }
}
