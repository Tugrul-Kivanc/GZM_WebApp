using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation.Configurations
{
    internal class TopNotesConfiguration : IEntityTypeConfiguration<TopNotes>
    {
        public void Configure(EntityTypeBuilder<TopNotes> builder)
        {
            builder.HasKey(a => new { a.PerfumeId, a.NoteId });
        }
    }
}
