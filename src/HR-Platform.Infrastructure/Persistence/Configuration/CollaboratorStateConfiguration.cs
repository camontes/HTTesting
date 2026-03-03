using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.CollaboratorStates;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorStateConfiguration : IEntityTypeConfiguration<CollaboratorState>
{
    public void Configure(EntityTypeBuilder<CollaboratorState> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            collaboratorStateId => collaboratorStateId.Value,
            value => new(value)
        );

        builder.Property(c => c.Name).HasMaxLength(50);

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
              
        
        builder.HasMany(c => c.Collaborators).WithOne(c => c.CollaboratorState).HasForeignKey(c => c.CollaboratorStateId);
    }
}
