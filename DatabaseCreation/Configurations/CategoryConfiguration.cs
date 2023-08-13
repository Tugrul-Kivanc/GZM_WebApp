using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(a => a.Products)
                .WithOne(b => b.Category)
                .HasForeignKey(c => c.CategoryId)
                .HasPrincipalKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasDefaultValue("Varsayılan Kategori")
                .HasMaxLength(50);

            builder.Property(a => a.Price)
                .IsRequired()
                .HasDefaultValue(0);
        }
    }
}
