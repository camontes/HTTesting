using HR_Platform.Domain.SurveyQuestionTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class SurveyQuestionTypeConfiguration : IEntityTypeConfiguration<SurveyQuestionType>
{
    public void Configure(EntityTypeBuilder<SurveyQuestionType> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(sqt => sqt.Id);
        builder.Property(sqt => sqt.Id).HasConversion(
            SurveyQuestionTypeId => SurveyQuestionTypeId.Value,
            value => new SurveyQuestionTypeId(value)
        );

        builder.Property(sqt => sqt.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(sqt => sqt.NameEnglish).HasMaxLength(50);

        builder.Property(sqt => sqt.IsEditable);

        builder.Property(sqt => sqt.IsDeleteable);

        //builder.HasMany(sqt => sqt.SurveyQuestions).WithOne(s => s.SurveyQuestionType).HasForeignKey(s => s.SurveyQuestionyTypeId);

    }
}
