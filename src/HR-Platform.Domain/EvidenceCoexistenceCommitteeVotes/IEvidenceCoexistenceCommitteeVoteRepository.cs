using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.EvidenceCoexistenceCommitteeVotes;

public interface IEvidenceCoexistenceCommitteeVoteRepository
{
    Task<EvidenceCoexistenceCommitteeVote?> GetByIdAsync(EvidenceCoexistenceCommitteeVoteId id);
    Task<EvidenceCoexistenceCommitteeVote?> GetNoneEvidenceCoexistenceCommitteeVoteByCompanyIdAsync(CompanyId companyId);
    Task<List<EvidenceCoexistenceCommitteeVote>?> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<bool> ExistsAsync(EvidenceCoexistenceCommitteeVoteId id);
    void Add(EvidenceCoexistenceCommitteeVote pension);
    void AddRange(List<EvidenceCoexistenceCommitteeVote> EvidenceCoexistenceCommitteeVotes);
    void Update(EvidenceCoexistenceCommitteeVote EvidenceCoexistenceCommitteeVote);
    void Delete(EvidenceCoexistenceCommitteeVote EvidenceCoexistenceCommitteeVote);
}
