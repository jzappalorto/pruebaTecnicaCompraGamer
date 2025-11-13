using System.ComponentModel.DataAnnotations;

namespace MiBackend.API.DTO
{
    public class MicroUpdateDto
    {
        [Required]
        public required string Patente { get; set; }

        [Required]
        public required string MarcaModelo { get; set; }
    }
}