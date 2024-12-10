using BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        }
    }
}
