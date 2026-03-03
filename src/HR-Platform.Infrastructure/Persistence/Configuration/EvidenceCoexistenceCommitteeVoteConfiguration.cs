using HR_Platform.Domain.EvidenceCoexistenceCommitteeVotes;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class EvidenceCoexistenceCommitteeVoteConfiguration : IEntityTypeConfiguration<EvidenceCoexistenceCommitteeVote>
{
    public void Configure(EntityTypeBuilder<EvidenceCoexistenceCommitteeVote> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultEvidenceCoexistenceCommitteeVoteId => DefaultEvidenceCoexistenceCommitteeVoteId.Value,
            value => new EvidenceCoexistenceCommitteeVoteId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.EvidenceCoexistenceCommitteeVotes).HasForeignKey(c => c.CompanyId);

        builder.Property(c => c.Name).HasMaxLength(50);
        builder.Property(c => c.NameEnglish).HasMaxLength(50);

        builder.Property(c => c.FileName).IsRequired();
        builder.Property(c => c.UrlFile).IsRequired();

        builder.Property(c => c.EmailWhoChangedByTH).HasMaxLength(50);
        builder.Property(c => c.NameWhoChangedByTH).HasMaxLength(50);
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
