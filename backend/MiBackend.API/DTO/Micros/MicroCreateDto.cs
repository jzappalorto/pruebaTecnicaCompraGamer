using System.ComponentModel.DataAnnotations;

namespace MiBackend.API.DTO
{
    public class MicroCreateDto
    {
        [Required]
        public required string Patente { get; set; }

        [Required]
        public required string MarcaModelo { get; set; }
    }
}