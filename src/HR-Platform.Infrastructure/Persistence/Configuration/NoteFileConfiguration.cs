using HR_Platform.Domain.NoteFiles;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class NoteFileConfiguration : IEntityTypeConfiguration<NoteFile>
{
    public void Configure(EntityTypeBuilder<NoteFile> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultNoteFileId => DefaultNoteFileId.Value,
            value => new NoteFileId(value)
        );

        builder.HasOne(p => p.Note).WithMany(c => c.NoteFiles).HasForeignKey(c => c.NoteId);

        builder.Property(c => c.FileName).HasMaxLength(50).IsRequired();
        builder.Property(c => c.UrlFile).IsRequired();

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
