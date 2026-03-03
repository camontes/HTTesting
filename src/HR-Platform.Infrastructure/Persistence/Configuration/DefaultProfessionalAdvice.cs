using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultProfessionalAdvices;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultProfessionalAdviceConfiguration : IEntityTypeConfiguration<DefaultProfessionalAdvice>
{
    public void Configure(EntityTypeBuilder<DefaultProfessionalAdvice> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultProfessionalAdviceId => DefaultProfessionalAdviceId.Value,
            value => new DefaultProfessionalAdviceId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.NameEnglish)
            .HasMaxLength(100);

        builder.Property(c => c.NameAcronyms)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(c => c.NameAcronymsEnglish)
            .HasMaxLength(10);

    }
}
