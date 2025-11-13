
using MiBackend.Domain.Entities.EntityBase;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiBackend.Domain.Entities
{
    public class Chofer : PersonaBase
    {
        // FK hacia Micro
        [ForeignKey(nameof(Micro))]
        public int? MicroId { get; set; }
        public Micro? Micro { get; set; }
    }
}
