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
        private readonly MiBackendContext _context;

        public ChicoService(MiBackendContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Chico>> GetAllAsync()
        {
            return await _context.Chicos.ToListAsync();
        }

        public async Task<Chico?> GetByIdAsync(int id)
        {
            return await _context.Chicos.FindAsync(id);
        }

        public async Task<Chico> CreateAsync(Chico chico)
        {
            _context.Chicos.Add(chico);
            await _context.SaveChangesAsync();
            return chico;
        }

        public async Task<Chico?> UpdateAsync(int id, Chico chico)
        {
            var existing = await _context.Chicos.FindAsync(id);
            if (existing == null)
                return null;

            _context.Entry(existing).CurrentValues.SetValues(chico);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var chico = await _context.Chicos.FindAsync(id);
            if (chico == null)
                return false;

            _context.Chicos.Remove(chico);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
