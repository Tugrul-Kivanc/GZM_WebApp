using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation.Configurations
{
    internal class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasKey(a => new { a.ProductId, a.OrderId });

            builder.HasOne(a => a.Product)
                .WithMany(b => b.ProductOrders)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.Order)
                .WithMany(b => b.ProductOrders)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
