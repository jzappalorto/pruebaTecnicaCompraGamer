namespace MiBackend.Domain.Entities.EntityBase
{
    public class PersonaBase
    {
        public int Id { get; set; }
        public required string DNI { get; set; }
        public required string Nombre { get; set; }
    }
}
