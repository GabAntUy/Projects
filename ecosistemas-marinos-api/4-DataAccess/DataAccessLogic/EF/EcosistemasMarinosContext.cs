using BusinessLogic.Entities;
using BusinessLogic.Entities.ValueObjects.Generic;
using DataAccessLogic.EF.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLogic.EF
{
    public class EcosistemasMarinosContext : DbContext
    {
        #region Dbset
        public DbSet<Log> Logs { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Amenaza> Amenazas { get; set; }
        public DbSet<Ecosistema> Ecosistemas { get; set; }
        public DbSet<Especie> Especies { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<EstadoDeConservacion> EstadosDeConservacion { get; set; }
        #endregion

        public EcosistemasMarinosContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EcosistemaConfiguration());
            modelBuilder.ApplyConfiguration(new EspecieConfiguration());
            modelBuilder.ApplyConfiguration(new EstadoDeConservacionConfiguration());
            modelBuilder.ApplyConfiguration(new PaisConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());

            base.OnModelCreating(modelBuilder);


        }
    }
}
