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
    public class EcosistemaConfiguration : IEntityTypeConfiguration<Ecosistema>
    {
        public void Configure(EntityTypeBuilder<Ecosistema> builder)
        {
            builder.HasQueryFilter(eco => eco.EstaActivo == true);

            builder.OwnsOne(e => e.Ubicacion);

            builder
                .OwnsMany(e => e.Imagenes)
                .HasKey( img => new{ img.Nombre, img.EcosistemaId});

            builder.HasOne(eco => eco.EstadoDeConservacion);

            builder
                .HasMany(eco => eco.EspeciesQuePuedenHabitarlo)
                .WithMany(esp => esp.PuedeHabitar)
                .UsingEntity<Dictionary<string, object>>(
                  "Ecosistema_Especie",
                  izq => izq.HasOne<Especie>().WithMany().OnDelete(DeleteBehavior.Restrict),
                  der => der.HasOne<Ecosistema>().WithMany().OnDelete(DeleteBehavior.Restrict)
                  );

            builder
                .HasMany(eco => eco.Paises)
                .WithMany(p => p.Ecosistemas)
                .UsingEntity<Dictionary<string, object>>(
                    "Ecosistema_Pais",
                    izq => izq.HasOne<Pais>().WithMany().OnDelete(DeleteBehavior.NoAction),
                    der => der.HasOne<Ecosistema>().WithMany().OnDelete(DeleteBehavior.NoAction)
                );

            var nombreConvert = new ValueConverter<Nombre, string>(
            v => v.Value,
            v => new Nombre(v)
            );
            //builder.OwnsOne(e => e.Nombre);
            builder.Property(e => e.Nombre).HasConversion(nombreConvert);
        }
    }
}
