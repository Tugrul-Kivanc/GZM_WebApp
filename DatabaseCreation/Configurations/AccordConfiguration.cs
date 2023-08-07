using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation.Configurations
{
    internal class AccordConfiguration : IEntityTypeConfiguration<Accord>
    {
        public void Configure(EntityTypeBuilder<Accord> builder)
        {
            builder.Property(a => a.Name)
                .IsRequired()
                .HasDefaultValue("Akor Yok")
                .HasMaxLength(50);
        }
    }
}
