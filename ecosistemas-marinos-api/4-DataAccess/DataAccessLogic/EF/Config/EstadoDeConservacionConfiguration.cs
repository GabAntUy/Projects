using BusinessLogic.Entities;
using BusinessLogic.Entities.ValueObjects.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.EF.Config
{
    public class EstadoDeConservacionConfiguration : IEntityTypeConfiguration<EstadoDeConservacion>
    {
        public void Configure(EntityTypeBuilder<EstadoDeConservacion> builder)
        {
            builder.OwnsOne(rc => rc.RangoConservacion);

            builder
                .HasIndex(rc => rc.Nombre)
                .IsUnique();

            var nombreConvert = new ValueConverter<Nombre, string>(
            v => v.Value,
            v => new Nombre(v)
            );
            builder.Property(ec => ec.Nombre).HasConversion(nombreConvert);
        }
    }
}
