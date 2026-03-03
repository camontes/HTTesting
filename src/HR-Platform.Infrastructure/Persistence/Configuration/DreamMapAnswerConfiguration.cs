using HR_Platform.Domain.DreamMapAnswers;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DreamMapAnswerConfiguration : IEntityTypeConfiguration<DreamMapAnswer>
{
    public void Configure(EntityTypeBuilder<DreamMapAnswer> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultDreamMapAnswerId => DefaultDreamMapAnswerId.Value,
            value => new DreamMapAnswerId(value)
        );

        builder.HasOne(c => c.CollaboratorDreamMapAnswer).WithMany(x => x.DreamMapAnswers).HasForeignKey(r => r.CollaboratorDreamMapAnswerId);

        builder.Property(c => c.Question)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.Answer)
            .HasMaxLength(500)
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

    }
}
