using HR_Platform.Domain.SurveyQuestions;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class SurveyQuestionConfiguration : IEntityTypeConfiguration<SurveyQuestion>
{
    public void Configure(EntityTypeBuilder<SurveyQuestion> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(sq => sq.Id);
        builder.Property(sq => sq.Id).HasConversion(
            SurveyQuestionId => SurveyQuestionId.Value,
            value => new SurveyQuestionId(value)
        );

        builder.HasOne(sq => sq.Survey).WithMany(s => s.SurveyQuestions).HasForeignKey(sq => sq.SurveyId);
        builder.HasOne(sq => sq.SurveyQuestionType).WithMany(sqt => sqt.SurveyQuestions).HasForeignKey(sq => sq.SurveyQuestionTypeId);
        builder.HasOne(sq => sq.SurveyQuestionMandatoryType).WithMany(sqmt => sqmt.SurveyQuestions).HasForeignKey(sq => sq.SurveyQuestionMandatoryTypeId);

        builder.Property(s => s.Text)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(s => s.IsEditable);

        builder.Property(s => s.IsDeleteable);

        builder.Property(s => s.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(s => s.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );
    }
}
