using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Transport.Models.Tablas;

namespace Transport.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Directorio> Directorios { get; set; }
        public DbSet<Planta> Plantas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<SolicitudTransporte> SolicitudesTransportes { get; set; }
        public DbSet<TipoLugar> TiposLugares { get; set; }
        public DbSet<TipoVehiculo> TiposVehiculos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cliente>().ToTable("T_Cliente");
            builder.Entity<Departamento>().ToTable("T_Departamento");
            builder.Entity<Directorio>().ToTable("T_Directorio");
            builder.Entity<Planta>().ToTable("T_Planta");
            builder.Entity<Producto>().ToTable("T_Producto");
            builder.Entity<SolicitudTransporte>().ToTable("T_SolicitudTransporte");
            builder.Entity<TipoLugar>().ToTable("T_TipoLugar");
            builder.Entity<TipoVehiculo>().ToTable("T_TipoVehiculo");
            builder.Entity<Vehiculo>().ToTable("T_Vehiculo");

            builder.Entity<ProductoAsignado>().ToTable("T_ProductoAsignado").
                HasKey(c => new { c.ProductoID, c.PlantaID });


            base.OnModelCreating(builder);
        }
    }
}
