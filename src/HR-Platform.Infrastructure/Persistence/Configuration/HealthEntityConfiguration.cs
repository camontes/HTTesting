using HR_Platform.Domain.HealthEntities;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class HealthEntityConfiguration : IEntityTypeConfiguration<HealthEntity>
{
    public void Configure(EntityTypeBuilder<HealthEntity> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasConversion(
            healthEntityId => healthEntityId.Value,
            value => new(value)
        );

        builder.HasOne(h => h.Company).WithMany(c => c.HealthEntities).HasForeignKey(p => p.CompanyId);


        builder.Property(h => h.Name).HasMaxLength(50);

        builder.Property(h => h.NameEnglish).HasMaxLength(50);


        builder.OwnsOne(h => h.Address, addressBuilder => {

            addressBuilder.Property(a => a.StreetAddress).HasMaxLength(100).IsRequired(false);

            addressBuilder.Property(a => a.CountryCode);
            addressBuilder.Property(a => a.Country).HasMaxLength(100).IsRequired(false);

            addressBuilder.Property(a => a.StateCode);
            addressBuilder.Property(a => a.State).HasMaxLength(100).IsRequired(false);

            addressBuilder.Property(a => a.CityCode);
            addressBuilder.Property(a => a.City).HasMaxLength(100).IsRequired(false);

            addressBuilder.Property(a => a.ZipCode).HasMaxLength(100).IsRequired(false);
        });

        builder.Property(h => h.IsEditable);

        builder.Property(h => h.IsDeleteable);

        builder.Property(h => h.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(h => h.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.HasMany(h => h.Collaborators).WithOne(c => c.HealthEntity).HasForeignKey(h => h.HealthEntityId);
    }
}
