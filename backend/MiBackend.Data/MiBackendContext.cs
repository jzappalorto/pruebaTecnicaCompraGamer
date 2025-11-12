using MiBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace MiBackend.Data
{
    public class MiBackendContext : DbContext
    {
        public MiBackendContext(DbContextOptions<MiBackendContext> options) : base(options)
        {
        }

        public required DbSet<Chico> Chicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chico>()
                .Property(c => c.DNI)
                .IsRequired();

            modelBuilder.Entity<Chico>()
                .Property(c => c.Nombre)
                .IsRequired();
        }
    }
}
