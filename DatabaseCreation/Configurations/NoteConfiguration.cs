using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation.Configurations
{
    internal class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasOne(a => a.Accord)
                .WithMany(b => b.Notes)
                .HasForeignKey(c => c.AccordId)
                .HasPrincipalKey(d => d.AccordId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasDefaultValue("?")
                .HasMaxLength(50);
        }
    }
}
