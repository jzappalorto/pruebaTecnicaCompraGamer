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
    public class ChoferService: IChoferService
    {
        private readonly MiBackendContext _db;

        public ChoferService(MiBackendContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Chofer>> GetAllAsync()
        {
            return await _db.Choferes
                .Include(c => c.Micro)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Chofer?> GetByIdAsync(int id)
        {
            return await _db.Choferes
                .Include(c => c.Micro)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Chofer?> GetByMicroAsync(int microId)
        {
            return await _db.Choferes
                .Include(c => c.Micro)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.MicroId == microId);
        }

        public async Task<Chofer> CreateAsync(Chofer chofer)
        {
            // Ya no se valida Micro aquí; la BD aplicará las restricciones pertinentes.
            _db.Choferes.Add(chofer);
            await _db.SaveChangesAsync();
            await _db.Entry(chofer).Reference(c => c.Micro).LoadAsync();
            return chofer;
        }

        public async Task<Chofer?> UpdateAsync(int id, Chofer chofer)
        {
            var existing = await _db.Choferes.FindAsync(id);
            if (existing == null) return null;

            // Si se intenta cambiar MicroId validar reglas 1:1
            if (chofer.MicroId != 0 && chofer.MicroId != existing.MicroId)
            {
                var micro = await _db.Micros.Include(m => m.Chofer).FirstOrDefaultAsync(m => m.Id == chofer.MicroId);
                if (micro == null) throw new ArgumentException($"No existe Micro con Id={chofer.MicroId}");
                if (micro.Chofer != null) throw new InvalidOperationException($"El Micro Id={micro.Id} ya tiene un Chofer asignado.");
                existing.MicroId = chofer.MicroId;
            }

            existing.DNI = chofer.DNI;
            existing.Nombre = chofer.Nombre;

            await _db.SaveChangesAsync();
            await _db.Entry(existing).Reference(c => c.Micro).LoadAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.Choferes.FindAsync(id);
            if (existing == null) return false;
            _db.Choferes.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignToMicroAsync(int choferId, int microId)
        {
            var chofer = await _db.Choferes.FindAsync(choferId);
            if (chofer == null) return false;

            var micro = await _db.Micros.Include(m => m.Chofer).FirstOrDefaultAsync(m => m.Id == microId);
            if (micro == null) return false;

            if (micro.Chofer != null && micro.Chofer.Id != choferId)
            {
                // El micro ya tiene otro chofer
                return false;
            }

            chofer.MicroId = microId;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
