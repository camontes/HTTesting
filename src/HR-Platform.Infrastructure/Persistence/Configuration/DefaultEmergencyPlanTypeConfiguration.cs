using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultEmergencyPlanTypes;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultEmergencyPlanTypeConfiguration : IEntityTypeConfiguration<DefaultEmergencyPlanType>
{
    public void Configure(EntityTypeBuilder<DefaultEmergencyPlanType> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultEmergencyPlanTypeId => DefaultEmergencyPlanTypeId.Value,
            value => new DefaultEmergencyPlanTypeId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
