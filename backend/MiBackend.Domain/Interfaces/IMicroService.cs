using MiBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiBackend.Domain.Interfaces
{
    public interface IMicroService
    {
        Task<Micro?> GetWithRelationsAsync(int id);
        Task<IEnumerable<Micro>> GetAllAsync();
        Task<Micro?> GetByIdAsync(int id);
        Task<Micro> CreateAsync(Micro micro);
        Task<Micro?> UpdateAsync(int id, Micro micro);
        Task<bool> DeleteAsync(int id);
    }
}
