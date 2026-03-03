using HR_Platform.Domain.Areas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class AreaConfiguration : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasConversion(
            AreaId => AreaId.Value,
            value => new AreaId(value)
        );

        builder.HasOne(a => a.Company).WithMany(c => c.Areas).HasForeignKey(a => a.CompanyId);

        builder.Property(a => a.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.NameEnglish).HasMaxLength(50);

        builder.Property(a => a.IsFormsVisible);

        builder.HasMany(a => a.Roles).WithOne(r => r.Area).HasForeignKey(r => r.AreaId);
        builder.HasMany(a => a.Forms).WithOne(r => r.NoveltyType).HasForeignKey(r => r.NoveltyTypeId);
        builder.HasMany(a => a.Surveys).WithOne(s => s.SurveyType).HasForeignKey(s => s.SurveyTypeId);
    }
}
