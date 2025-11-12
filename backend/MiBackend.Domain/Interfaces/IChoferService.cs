using MiBackend.Domain.Entities;

namespace MiBackend.Domain.Interfaces
{
    public interface IChoferService
    {
        Task<IEnumerable<Chofer>> GetAllAsync();
        Task<Chofer?> GetByIdAsync(int id);
        Task<Chofer?> GetByMicroAsync(int microId);
        Task<Chofer> CreateAsync(Chofer chofer);
        Task<Chofer?> UpdateAsync(int id, Chofer chofer);
        Task<bool> DeleteAsync(int id);

        // Asignar chofer a un micro (1:1). Devuelve false si el micro ya tiene chofer distinto.
        Task<bool> AssignToMicroAsync(int choferId, int microId);
    }
}