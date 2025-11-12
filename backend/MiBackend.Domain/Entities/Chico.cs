using MiBackend.Domain.Entities.EntityBase;

namespace MiBackend.Domain.Entities
{
    public class Chico:PersonaBase
    {
        // FK hacia Micro (muchos Chico -> 1 Micro)
        public int MicroId { get; set; }
        public Micro Micro { get; set; } = null!;
    }
}
