using HR_Platform.Domain.BrigadeMembers;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class BrigadeMemberConfiguration : IEntityTypeConfiguration<BrigadeMember>
{
    public void Configure(EntityTypeBuilder<BrigadeMember> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultBrigadeMemberId => DefaultBrigadeMemberId.Value,
            value => new BrigadeMemberId(value)
        );

        //Collaborator
        builder.HasOne(cl => cl.Collaborator)
        .WithMany(c => c.BrigadeMembers)
        .HasForeignKey(cl => cl.CollaboratorId);

        //Brigade Adjustment
        builder.HasOne(l => l.BrigadeAdjustment)
        .WithMany(ln => ln.BrigadeMembers)
        .HasForeignKey(l => l.BrigadeAdjustmentId);

        builder.Property(r => r.HasBeenDeletedBrigadeAdjustment);

        builder.Property(r => r.IsMainLeader);
        builder.Property(r => r.IsBrigadeLeader);

        builder.Property(r => r.IsVisible);

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
