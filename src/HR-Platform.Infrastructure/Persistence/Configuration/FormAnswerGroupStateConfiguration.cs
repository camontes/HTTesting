using HR_Platform.Domain.FormAnswerGroupStates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class FormAnswerGroupStateConfiguration : IEntityTypeConfiguration<FormAnswerGroupState>
{
    public void Configure(EntityTypeBuilder<FormAnswerGroupState> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(fag => fag.Id);
        builder.Property(fag => fag.Id).HasConversion(
            FormAnswerGroupStateId => FormAnswerGroupStateId.Value,
            value => new FormAnswerGroupStateId(value)
        );

        builder.Property(fags => fags.Name).HasMaxLength(50).IsRequired();
        builder.Property(fags => fags.NameEnglish).HasMaxLength(50).IsRequired();

        builder.HasMany(fags => fags.FormAnswerGroups).WithOne(fag => fag.FormAnswerGroupState).HasForeignKey(fag => fag.FormAnswerGroupStateId);
    }
}
