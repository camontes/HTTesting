using HR_Platform.Domain.CollaboratorSoftSkills;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorSoftSkillConfiguration : IEntityTypeConfiguration<CollaboratorSoftSkill>
{
    public void Configure(EntityTypeBuilder<CollaboratorSoftSkill> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultCollaboratorSoftSkillId => DefaultCollaboratorSoftSkillId.Value,
            value => new CollaboratorSoftSkillId(value)
        );

        //Collaborator
        builder.HasOne(cl => cl.Collaborator)
        .WithMany(c => c.CollaboratorSoftSkills)
        .HasForeignKey(cl => cl.CollaboratorId);

        //Soft Skill Name
        builder.HasOne(l => l.DefaultSoftSkill)
        .WithMany(ln => ln.CollaboratorSoftSkills)
        .HasForeignKey(l => l.DefaultSoftSkillId);

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
