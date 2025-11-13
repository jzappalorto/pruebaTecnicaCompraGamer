using System.ComponentModel.DataAnnotations;

namespace MiBackend.API.DTO
{
    public class ChoferUpdateDto
    {
        [Required]
        public required string DNI { get; set; }

        [Required]
        public required string Nombre { get; set; }

        public int? MicroId { get; set; }
    }
}