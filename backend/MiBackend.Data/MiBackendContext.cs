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
        public required DbSet<Chofer> Choferes { get; set; }
        public required DbSet<Micro> Micros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //-------------------------------------------------------------
            //entidad Chico
            modelBuilder.Entity<Chico>()
                .Property(c => c.DNI)
                .IsRequired();

            modelBuilder.Entity<Chico>()
                .Property(c => c.Nombre)
                .IsRequired();
            modelBuilder.Entity<Chico>()
                .HasIndex(m => m.DNI)
                .IsUnique();
            //--------------------------------------------------------------
            //entidad Chofer
            modelBuilder.Entity<Chofer>()
                .Property(c => c.DNI)
                .IsRequired();

            modelBuilder.Entity<Chofer>()
                .Property(c => c.Nombre)
                .IsRequired();
            modelBuilder.Entity<Chofer>()
                .HasIndex(m => m.DNI)
                .IsUnique();
            //-------------------------------------------------------------
            //entidad Micro
            modelBuilder.Entity<Micro>()
                .Property(c => c.Patente)
                .IsRequired();

            //-------------------------------------------------------------
            // Relación 1:1 entre Micro y Chofer — FK opcional en Chofer.
            modelBuilder.Entity<Micro>()
                .HasOne(m => m.Chofer)
                .WithOne(c => c.Micro)
                .HasForeignKey<Chofer>(c => c.MicroId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // índice único en Patente (garantiza restricción en la base de datos)
            modelBuilder.Entity<Micro>()
                .HasIndex(m => m.Patente)
                .IsUnique();
        }
    }
}
