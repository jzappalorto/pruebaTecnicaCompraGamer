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
    }
}
