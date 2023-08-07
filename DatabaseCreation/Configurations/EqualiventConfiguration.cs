using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation.Configurations
{
    internal class EqualiventConfiguration : IEntityTypeConfiguration<Equalivent>
    {
        public void Configure(EntityTypeBuilder<Equalivent> builder)
        {
            builder.HasOne(a => a.Perfume)
                .WithMany(b => b.Equalivents)
                .HasForeignKey(c => c.PerfumeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(a => a.Code)
                .IsRequired()
                .HasDefaultValue("-")
                .HasMaxLength(50);
        }
    }
}
