using System.Collections.Generic;

namespace MiBackend.API.DTO
{
    public class MicroReadDto
    {
        public int Id { get; set; }
        public required string Patente { get; set; }
        public required string MarcaModelo { get; set; }

        public ChoferReadDto? Chofer { get; set; }
        public IEnumerable<ChicoReadDto>? Chicos { get; set; }
    }
}