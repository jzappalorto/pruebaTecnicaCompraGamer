using MiBackend.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiBackend.Domain.Interfaces
{
    public interface IChicoService
    {
        Task<IEnumerable<Chico>> GetAllAsync();
        Task<IEnumerable<Chico>> GetAllByMicroAsync(int microId);
        Task<Chico?> GetByIdAsync(int id);
        Task<Chico> CreateAsync(Chico chico);
        Task<Chico?> UpdateAsync(int id, Chico chico);
        Task<bool> DeleteAsync(int id);

        // Asignar un chico a un micro (valida existencia de micro)
        Task<bool> AssignToMicroAsync(int chicoId, int microId);

    }
}
