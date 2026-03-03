using HR_Platform.Domain.SurveyQuestionMandatoryTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class SurveyQuestionMandatoryTypeConfiguration : IEntityTypeConfiguration<SurveyQuestionMandatoryType>
{
    public void Configure(EntityTypeBuilder<SurveyQuestionMandatoryType> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(smt => smt.Id);
        builder.Property(smt => smt.Id).HasConversion(
            SurveyQuestionMandatoryTypeId => SurveyQuestionMandatoryTypeId.Value,
            value => new SurveyQuestionMandatoryTypeId(value)
        );

        builder.Property(smt => smt.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(smt => smt.NameEnglish).HasMaxLength(50);

        builder.Property(smt => smt.IsEditable);

        builder.Property(smt => smt.IsDeleteable);

        builder.HasMany(smt => smt.SurveyQuestions).WithOne(s => s.SurveyQuestionMandatoryType).HasForeignKey(s => s.SurveyQuestionMandatoryTypeId);

    }
}
