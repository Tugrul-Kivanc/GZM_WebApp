using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(a => a.Name)
                .IsRequired()
                .HasDefaultValue("Varsayılan Ürün")
                .HasMaxLength(50);

            builder.Property(a => a.Stock)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(a => a.Price)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(a => a.TotalSales)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(a => a.Type)
                .IsRequired()
                .HasDefaultValue("Varsayılan Ürün Tipi")
                .HasMaxLength(50);
        }
    }
}
