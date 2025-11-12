using MiBackend.Domain.Entities;
using MiBackend.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiBackend.Data.Services
{
    public class MicroService:IMicroService
    {
        private readonly MiBackendContext _db;

        public MicroService(MiBackendContext db)
        {
            _db = db;
        }

        public async Task<Micro?> GetWithRelationsAsync(int id)
        {
            return await _db.Micros
                .Include(m => m.Chofer)
                .Include(m => m.Chicos)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
