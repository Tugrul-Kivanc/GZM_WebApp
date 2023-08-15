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
            builder.HasMany(a => a.Notes)
                .WithOne(b => b.Accord)
                .HasForeignKey(c => c.AccordId)
                .HasPrincipalKey(d => d.AccordId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(a => a.Name)
                .HasDefaultValue("Varsayılan Akor")
                .HasMaxLength(50);

            builder.Property(a => a.Description)
                .HasDefaultValue("")
                .IsRequired(false);
        }
    }
}
