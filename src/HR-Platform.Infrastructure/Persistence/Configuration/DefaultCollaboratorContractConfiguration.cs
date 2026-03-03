using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultCollaboratorContracts;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultCollaboratorContractConfiguration : IEntityTypeConfiguration<DefaultCollaboratorContract>
{
    public void Configure(EntityTypeBuilder<DefaultCollaboratorContract> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultCollaboratorContractId => DefaultCollaboratorContractId.Value,
            value => new DefaultCollaboratorContractId(value)
        );

        builder.Property(c => c.Arl).HasMaxLength(50);
        builder.Property(c => c.Bonus).HasMaxLength(200);
        builder.Property(c => c.ContractType).HasMaxLength(50);
        builder.Property(c => c.Salary).HasMaxLength(10);

    }
}
