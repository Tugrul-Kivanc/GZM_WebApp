using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation.Configurations
{
    internal class MidNotesConfiguration : IEntityTypeConfiguration<MidNotes>
    {
        public void Configure(EntityTypeBuilder<MidNotes> builder)
        {
            builder.HasKey(a => new { a.PerfumeId, a.NoteId });
        }
    }
}
