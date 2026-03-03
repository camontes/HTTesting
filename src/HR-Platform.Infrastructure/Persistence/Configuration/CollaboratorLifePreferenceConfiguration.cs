using HR_Platform.Domain.CollaboratorLifePreferences;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorLifePreferenceConfiguration : IEntityTypeConfiguration<CollaboratorLifePreference>
{
    public void Configure(EntityTypeBuilder<CollaboratorLifePreference> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultCollaboratorLifePreferenceId => DefaultCollaboratorLifePreferenceId.Value,
            value => new CollaboratorLifePreferenceId(value)
        );

        //Collaborator
        builder.HasOne(cl => cl.Collaborator)
        .WithMany(c => c.CollaboratorLifePreferences)
        .HasForeignKey(cl => cl.CollaboratorId);

        //Soft Skill Name
        builder.HasOne(l => l.DefaultLifePreference)
        .WithMany(ln => ln.CollaboratorLifePreferences)
        .HasForeignKey(l => l.DefaultLifePreferenceId);

        builder.Property(c => c.OtherLanguageName).HasMaxLength(50);

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
