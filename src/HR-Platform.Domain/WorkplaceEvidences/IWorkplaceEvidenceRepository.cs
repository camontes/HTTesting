using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.WorkplaceEvidences;

public interface IWorkplaceEvidenceRepository : ISearchFilterRepository<WorkplaceEvidence>
{
    Task<WorkplaceEvidence?> GetByIdAsync(WorkplaceEvidenceId id);
    Task<List<WorkplaceEvidence>?> GetByCollaboratorIdAsync(CollaboratorId collaboratorId, string year);
    Task<bool> ExistsAsync(WorkplaceEvidenceId id);
    void Add(WorkplaceEvidence WorkplaceEvidence);
    void AddRange(List<WorkplaceEvidence> WorkplaceEvidences);
    void Update(WorkplaceEvidence WorkplaceEvidence);
    void Delete(WorkplaceEvidence WorkplaceEvidence);
    void DeleteRange(List<WorkplaceEvidence> WorkplaceEvidences);
}
