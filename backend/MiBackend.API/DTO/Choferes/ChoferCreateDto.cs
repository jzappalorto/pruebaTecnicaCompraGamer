using System.ComponentModel.DataAnnotations;

namespace MiBackend.API.DTO
{
    public class ChoferCreateDto
    {
        public required string DNI { get; set; }
        public required string Nombre { get; set; }
        public int MicroId { get; set; }
    }
}
