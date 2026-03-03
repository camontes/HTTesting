using HR_Platform.Domain.Surveys;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasConversion(
            SurveyId => SurveyId.Value,
            value => new SurveyId(value)
        );

        builder.HasOne(s => s.Company).WithMany(c => c.Surveys).HasForeignKey(c => c.CompanyId);
        builder.HasOne(s => s.SurveyType).WithMany(a => a.Surveys).HasForeignKey(c => c.SurveyTypeId);

        builder.Property(s => s.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.Description)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(s => s.EmailWhoChangedByTH)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.NameWhoChangedByTH)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.IsVisible);

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
