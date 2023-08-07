using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation.Configurations
{
    internal class EqualiventBrandConfiguration : IEntityTypeConfiguration<EqualiventBrand>
    {
        public void Configure(EntityTypeBuilder<EqualiventBrand> builder)
        {
            builder.HasMany(a => a.Equalivents)
                .WithOne(b => b.EqualiventBrand)
                .HasForeignKey(c => c.EqualiventBrandId)
                .HasPrincipalKey(d => d.EqualiventBrandId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasDefaultValue("Muadil Marka")
                .HasMaxLength(50);
        }
    }
}
