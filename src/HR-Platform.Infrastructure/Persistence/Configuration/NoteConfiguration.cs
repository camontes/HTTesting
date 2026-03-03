using HR_Platform.Domain.Notes;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultNoteId => DefaultNoteId.Value,
            value => new NoteId(value)
        );

        builder.HasOne(p => p.Creator).WithMany(c => c.CreatedNotes).HasForeignKey(c => c.CreatedBy);
        builder.HasOne(p => p.Assignee).WithMany(c => c.AssigneeNotes).HasForeignKey(c => c.AssignedTo);
        builder.HasOne(p => p.ParentNote).WithMany(c => c.Replies).HasForeignKey(c => c.ParentNoteId);


        builder.Property(c => c.Description).IsRequired().HasMaxLength(10000);
        builder.Property(c => c.IsPublic);

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

        builder.HasMany(c => c.Viewers).WithOne(n => n.Note).HasForeignKey(n => n.NoteId);
    }
}
