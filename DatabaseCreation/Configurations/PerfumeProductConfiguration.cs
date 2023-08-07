using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation.Configurations
{
    internal class PerfumeProductConfiguration : IEntityTypeConfiguration<PerfumeProduct>
    {
        public void Configure(EntityTypeBuilder<PerfumeProduct> builder)
        {
            builder.HasKey(a => new {a.ProductId, a.PerfumeId});

            builder.HasOne(a => a.Perfume)
                .WithOne(b => b.Product);
        }
    }
}
