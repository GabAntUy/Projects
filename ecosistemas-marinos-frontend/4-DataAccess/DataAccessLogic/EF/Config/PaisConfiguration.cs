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
    internal class PaisConfiguration : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder
                .HasIndex(p => p.Nombre)
                .IsUnique();
            builder
                .HasIndex(p => p.CodigoIso)
                .IsUnique();
        }
    }
}
