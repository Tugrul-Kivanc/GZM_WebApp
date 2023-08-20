using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(a => a.ProductCount)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(a => a.Fee)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(a => a.Payment)
                .IsRequired()
                .HasDefaultValue("Nakit")
                .HasMaxLength(10);

            builder.Property(a => a.Description)
                .HasDefaultValue("");
        }
    }
}
