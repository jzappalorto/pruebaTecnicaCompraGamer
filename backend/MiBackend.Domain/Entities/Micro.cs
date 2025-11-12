
namespace MiBackend.Domain.Entities
{
    public class Micro
    {
        public int Id { get; set; }
        public required string Patente {  get; set; }
        public required string MarcaModelo { get; set; }
                
        public ICollection<Chico> Chicos { get; set; } = new List<Chico>();
        public Chofer? Chofer { get; set; }
    }
}
