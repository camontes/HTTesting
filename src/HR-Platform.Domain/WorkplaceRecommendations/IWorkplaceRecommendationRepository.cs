using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.WorkplaceRecommendations;

public interface IWorkplaceRecommendationRepository : ISearchFilterRepository<WorkplaceRecommendation>
{
    Task<WorkplaceRecommendation?> GetByIdAsync(WorkplaceRecommendationId id);
    Task<List<WorkplaceRecommendation>?> GetByCollaboratorIdAsync(CollaboratorId collaboratorId, string Year);
    Task<bool> ExistsAsync(WorkplaceRecommendationId id);
    void Add(WorkplaceRecommendation WorkplaceRecommendation);
    void AddRange(List<WorkplaceRecommendation> WorkplaceRecommendations);
    void Update(WorkplaceRecommendation WorkplaceRecommendation);
    void Delete(WorkplaceRecommendation WorkplaceRecommendation);
    void DeleteRange(List<WorkplaceRecommendation> WorkplaceRecommendations);
}
