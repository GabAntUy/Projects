using BusinessLogic.Entities;
using BusinessLogic.Entities.ValueObjects.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.EF.Config
{
    public class EspecieConfiguration : IEntityTypeConfiguration<Especie>
    {
        public void Configure(EntityTypeBuilder<Especie> builder)
        {
            builder
                .HasOne(esp => esp.Habita)
                .WithMany(eco => eco.EspeciesQueLoHabitan)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(esp => esp.EstadoConservacion);

            builder
                .OwnsMany(e => e.Imagenes)
                .HasKey(esp => new { esp.Nombre, esp.EspecieId});

            builder.OwnsOne(esp => esp.RangoPeso);
            builder.OwnsOne(esp => esp.RangoLargo);

            var nombreVulgarConvert = new ValueConverter<Nombre, string>(
                v => v.Value,
                v => new Nombre(v)
            );
            var nombreCientificoConvert = new ValueConverter<Nombre, string>(
                v => v.Value,
                v => new Nombre(v)
            );
            builder.Property(e => e.NombreVulgar).HasConversion(nombreVulgarConvert);
            builder.Property(e => e.NombreCientifico).HasConversion(nombreCientificoConvert);
            //builder.HasOne(esp => esp.EstadoConservacion).WithMany(esp => esp.Especies).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
