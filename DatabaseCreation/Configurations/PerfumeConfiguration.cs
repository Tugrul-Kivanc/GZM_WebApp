using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation.Configurations
{
    internal class PerfumeConfiguration : IEntityTypeConfiguration<Perfume>
    {
        public void Configure(EntityTypeBuilder<Perfume> builder)
        {
            builder.HasMany(a => a.Notes)
                .WithMany(b => b.Perfumes);

            builder.Property(a => a.Code)
                .IsRequired()
                .HasDefaultValue("")
                .HasMaxLength(10);

            builder.Property(a => a.Brand)
                .IsRequired()
                .HasDefaultValue("Bilinmeyen Marka")
                .HasMaxLength(50);

            builder.Property(a => a.Type)
                .IsRequired()
                .HasDefaultValue("Bilinmeyen Tip")
                .HasMaxLength(50);

            builder.Property(a => a.Smell)
                .IsRequired()
                .HasDefaultValue("?")
                .HasMaxLength(50);

            builder.Property(a => a.Gender)
                .IsRequired()
                .HasDefaultValue("Unisex")
                .HasMaxLength(10);

            builder.Property(a => a.Weather)
                .HasDefaultValue("")
                .HasMaxLength(10);

            builder.Property(a => a.Description)
                .HasDefaultValue("");

            builder.Property(a => a.Link)
                .HasDefaultValue("");

            builder.Property(a => a.Sillage)
                .HasDefaultValue(0);
        }
    }
}
