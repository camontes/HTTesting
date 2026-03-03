using HR_Platform.Domain.MasterUsers;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class MasterUserConfiguration : IEntityTypeConfiguration<MasterUser>
{
    public void Configure(EntityTypeBuilder<MasterUser> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasConversion(
            masterUserId => masterUserId.Value,
            value => new MasterUserId(value)
        );

        builder.Property(m => m.Email).HasConversion(
           email => email.Value,
           value => Email.Create(value)!)
           .HasMaxLength(100);
        builder.HasIndex(c => c.Email).IsUnique();

        builder.Property(m => m.Name).HasMaxLength(100);

        builder.Property(m => m.NameEnglish).HasMaxLength(100);

        builder.Property(m => m.PhoneNumber).HasConversion(
          phoneNumber => phoneNumber.Value,
          value => PhoneNumber.Create(value)!)
          .HasMaxLength(50);

        builder.Property(m => m.Photo);
        builder.Property(m => m.PhotoName);

        builder.Property(m => m.RoleName).HasMaxLength(100);

        builder.Property(m => m.RoleNameEnglish).HasMaxLength(100);

        builder.Property(m => m.LoginCode).HasMaxLength(10);

        builder.Property(m => m.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(m => m.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );
    }
}
