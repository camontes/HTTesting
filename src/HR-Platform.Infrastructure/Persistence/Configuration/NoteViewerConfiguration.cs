using HR_Platform.Domain.NoteViewers;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class NoteViewerConfiguration : IEntityTypeConfiguration<NoteViewer>
{
    public void Configure(EntityTypeBuilder<NoteViewer> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultNoteViewerId => DefaultNoteViewerId.Value,
            value => new NoteViewerId(value)
        );

        builder.HasOne(nv => nv.Note).WithMany(n => n.Viewers).HasForeignKey(nv => nv.NoteId);
        builder.HasOne(nv => nv.Viewer).WithMany(t => t.NoteViewers).HasForeignKey(nv => nv.ViewerId);

        builder.Property(r => r.IsEditable);

        builder.Property(r => r.IsDeleteable);

        builder.Property(r => r.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );
    }
}
