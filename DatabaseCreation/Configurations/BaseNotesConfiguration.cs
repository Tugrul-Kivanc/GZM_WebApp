using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation.Configurations
{
    internal class BaseNotesConfiguration : IEntityTypeConfiguration<BaseNotes>
    {
        public void Configure(EntityTypeBuilder<BaseNotes> builder)
        {
            builder.HasKey(a => new { a.PerfumeId, a.NoteId });
        }
    }
}
