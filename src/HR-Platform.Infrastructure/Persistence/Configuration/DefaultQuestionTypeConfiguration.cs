using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultQuestionTypes;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultQuestionTypeConfiguration : IEntityTypeConfiguration<DefaultQuestionType>
{
    public void Configure(EntityTypeBuilder<DefaultQuestionType> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultQuestionTypeId => DefaultQuestionTypeId.Value,
            value => new DefaultQuestionTypeId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
