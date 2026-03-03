using HR_Platform.Domain.CollaboratorLanguages;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorLanguageConfiguration : IEntityTypeConfiguration<CollaboratorLanguage>
{
    public void Configure(EntityTypeBuilder<CollaboratorLanguage> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultCollaboratorLanguageId => DefaultCollaboratorLanguageId.Value,
            value => new CollaboratorLanguageId(value)
        );

        builder.HasOne(cl => cl.Collaborator)
        .WithMany(c => c.CollaboratorLanguages)
        .HasForeignKey(cl => cl.CollaboratorId);


        //Language Name
        builder.HasOne(l => l.DefaultLanguageType)
        .WithMany(ln => ln.CollaboratorLanguages)
        .HasForeignKey(l => l.DefaultLanguageTypeId);

        //Language Level
        builder.HasOne(l => l.DefaultLanguageLevel)
        .WithMany(ll => ll.CollaboratorLanguages)
        .HasForeignKey(l => l.DefaultLanguageLevelId);

        builder.Property(c => c.OtherLanguageName)
            .HasMaxLength(50);

        builder.Property(c => c.OtherLanguageNameEnglish).HasMaxLength(50);

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
