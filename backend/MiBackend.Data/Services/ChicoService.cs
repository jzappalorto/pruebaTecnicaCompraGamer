using MiBackend.Data;
using MiBackend.Domain.Entities;
using MiBackend.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiBackend.Data.Services
{
    public class ChicoService : IChicoService
    {
        private readonly MiBackendContext _db;

        public ChicoService(MiBackendContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Chico>> GetAllAsync()
        {
            return await _db.Chicos
                .Include(c => c.Micro)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Chico>> GetAllByMicroAsync(int microId)
        {
            return await _db.Chicos
                .Where(c => c.MicroId == microId)
                .Include(c => c.Micro)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Chico?> GetByIdAsync(int id)
        {
            return await _db.Chicos
                .Include(c => c.Micro)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Chico> CreateAsync(Chico chico)
        {
            // Si se especificó MicroId, validar que exista
            if (chico.MicroId != 0)
            {
                var microExists = await _db.Micros.AnyAsync(m => m.Id == chico.MicroId);
                if (!microExists) throw new ArgumentException($"No existe Micro con Id={chico.MicroId}");
            }

            _db.Chicos.Add(chico);
            await _db.SaveChangesAsync();
            // Cargar navegación antes de devolver
            await _db.Entry(chico).Reference(c => c.Micro).LoadAsync();
            return chico;
        }

        public async Task<Chico?> UpdateAsync(int id, Chico chico)
        {
            var existing = await _db.Chicos.FindAsync(id);
            if (existing == null) return null;

            // Validar Micro si se cambió
            if (chico.MicroId != 0 && chico.MicroId != existing.MicroId)
            {
                var microExists = await _db.Micros.AnyAsync(m => m.Id == chico.MicroId);
                if (!microExists) throw new ArgumentException($"No existe Micro con Id={chico.MicroId}");
                existing.MicroId = chico.MicroId;
            }

            // Actualizar campos base de PersonaBase
            existing.DNI = chico.DNI;
            existing.Nombre = chico.Nombre;

            await _db.SaveChangesAsync();
            await _db.Entry(existing).Reference(c => c.Micro).LoadAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.Chicos.FindAsync(id);
            if (existing == null) return false;
            _db.Chicos.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignToMicroAsync(int chicoId, int microId)
        {
            var chico = await _db.Chicos.FindAsync(chicoId);
            if (chico == null) return false;

            var micro = await _db.Micros.FindAsync(microId);
            if (micro == null) return false;

            chico.MicroId = microId;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
