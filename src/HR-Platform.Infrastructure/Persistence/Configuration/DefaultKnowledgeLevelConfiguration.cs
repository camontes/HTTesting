using HR_Platform.Domain.DefaultKnowledgeLevels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultKnowledgeLevelConfiguration : IEntityTypeConfiguration<DefaultKnowledgeLevel>
{
    public void Configure(EntityTypeBuilder<DefaultKnowledgeLevel> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultKnowledgeLevelId => DefaultKnowledgeLevelId.Value,
            value => new DefaultKnowledgeLevelId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
