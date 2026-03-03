using HR_Platform.Domain.DreamMapQuestions;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DreamMapQuestionConfiguration : IEntityTypeConfiguration<DreamMapQuestion>
{
    public void Configure(EntityTypeBuilder<DreamMapQuestion> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultDreamMapQuestionId => DefaultDreamMapQuestionId.Value,
            value => new DreamMapQuestionId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.DreamMapQuestions).HasForeignKey(c => c.CompanyId);

        builder.Property(c => c.Question)
            .HasMaxLength(200)
            .IsRequired();

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
        //builder.HasMany(c => c.DreamMapAnswers).WithOne(ct => ct.DreamMapQuestion).HasForeignKey(ct => ct.DreamMapQuestionId);

    }
}
