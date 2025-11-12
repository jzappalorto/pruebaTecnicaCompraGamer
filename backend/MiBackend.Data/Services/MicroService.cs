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

        public async Task<IEnumerable<Micro>> GetAllAsync()
        {
            return await _db.Micros
                .Include(m => m.Chofer)
                .Include(m => m.Chicos)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Micro?> GetByIdAsync(int id)
        {
            return await _db.Micros
                .Include(m => m.Chofer)
                .Include(m => m.Chicos)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Micro> CreateAsync(Micro micro)
        {
            _db.Micros.Add(micro);
            await _db.SaveChangesAsync();
            await _db.Entry(micro).Reference(m => m.Chofer).LoadAsync();
            await _db.Entry(micro).Collection(m => m.Chicos).LoadAsync();
            return micro;
        }

        public async Task<Micro?> UpdateAsync(int id, Micro micro)
        {
            var existing = await _db.Micros.FindAsync(id);
            if (existing == null) return null;

            existing.Patente = micro.Patente;
            existing.MarcaModelo = micro.MarcaModelo;

            await _db.SaveChangesAsync();
            await _db.Entry(existing).Reference(m => m.Chofer).LoadAsync();
            await _db.Entry(existing).Collection(m => m.Chicos).LoadAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.Micros.FindAsync(id);
            if (existing == null) return false;

            var hasChofer = await _db.Choferes.AnyAsync(c => c.MicroId == id);
            var hasChicos = await _db.Chicos.AnyAsync(c => c.MicroId == id);
            if (hasChofer || hasChicos) throw new InvalidOperationException("El Micro tiene Chofer o Chicos asociados y no puede eliminarse.");

            _db.Micros.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
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
