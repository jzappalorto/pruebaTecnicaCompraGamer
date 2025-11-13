namespace MiBackend.API.DTO
{
    public class ChicoReadDto
    {
        public int Id { get; set; }
        public required string DNI { get; set; }
        public required string Nombre { get; set; }
        public int MicroId { get; set; }
    }
}