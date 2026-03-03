using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.BloodTypes;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class BloodTypeConfiguration : IEntityTypeConfiguration<BloodType>
{
    public void Configure(EntityTypeBuilder<BloodType> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            defaultRoleId => defaultRoleId.Value,
            value => new BloodTypeId(value)
        );

        builder.Property(c => c.Name).HasMaxLength(100);

        builder.Property(c => c.NameEnglish).HasMaxLength(100);
    }
}
