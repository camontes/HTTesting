using HR_Platform.Domain.DefaultSoftSkills;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultSoftSkillConfiguration : IEntityTypeConfiguration<DefaultSoftSkill>
{
    public void Configure(EntityTypeBuilder<DefaultSoftSkill> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultSoftSkillId => DefaultSoftSkillId.Value,
            value => new DefaultSoftSkillId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
