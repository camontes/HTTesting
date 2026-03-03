using HR_Platform.Domain.WorkplaceRecommendations;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class WorkplaceRecommendationConfiguration : IEntityTypeConfiguration<WorkplaceRecommendation>
{
    public void Configure(EntityTypeBuilder<WorkplaceRecommendation> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultWorkplaceRecommendationId => DefaultWorkplaceRecommendationId.Value,
            value => new WorkplaceRecommendationId(value)
        );

        builder.HasOne(p => p.Collaborator).WithMany(c => c.WorkplaceRecommendations).HasForeignKey(c => c.CollaboratorId);

        builder.Property(c => c.FileName).HasMaxLength(200).IsRequired();

        builder.Property(c => c.UrlFile).IsRequired();

        builder.Property(c => c.NameWhoChangedByTH).HasMaxLength(50);

        builder.Property(c => c.EmailWhoChangedByTH).HasMaxLength(50);

        builder.Property(c => c.UrlPhotoWhoChangedByTH);

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
