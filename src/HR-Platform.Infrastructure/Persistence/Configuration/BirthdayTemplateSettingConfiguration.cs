using HR_Platform.Domain.BirthdayTemplateSettings;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class BirthdayTemplateSettingConfiguration : IEntityTypeConfiguration<BirthdayTemplateSetting>
{
    public void Configure(EntityTypeBuilder<BirthdayTemplateSetting> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasConversion(
            roleId => roleId.Value,
            value => new BirthdayTemplateSettingId(value)
        );

        builder.HasOne(r => r.Company).WithMany(c => c.BirthdayTemplateSettings).HasForeignKey(r => r.CompanyId);

        builder.Property(r => r.IsDefaultTemplate);
        builder.Property(r => r.IsAllowSendPost);

        builder.Property(r => r.CustomMessage);
        builder.Property(r => r.CustomTemplateURL);
        builder.Property(r => r.CustomTemplateName);

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
