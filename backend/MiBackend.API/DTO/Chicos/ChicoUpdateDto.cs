namespace MiBackend.API.DTO.Chicos
{
    public class ChicoUpdateDto
    {
        public required string DNI { get; set; }
        public required string Nombre { get; set; }
        public int MicroId { get; set; }
    }
}
