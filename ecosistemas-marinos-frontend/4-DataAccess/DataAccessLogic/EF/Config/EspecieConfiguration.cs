using BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

            //builder.HasOne(esp => esp.EstadoConservacion).WithMany(esp => esp.Especies).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
