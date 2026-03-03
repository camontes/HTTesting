using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.OccupationalRecommendations;

public interface IOccupationalRecommendationRepository : ISearchFilterRepository<OccupationalRecommendation>
{
    Task<OccupationalRecommendation?> GetByIdAsync(OccupationalRecommendationId id);
    Task<List<OccupationalRecommendation>?> GetByCollaboratorIdAsync(CollaboratorId collaboratorId, string year);
    Task<bool> ExistsAsync(OccupationalRecommendationId id);
    void Add(OccupationalRecommendation OccupationalRecommendation);
    void AddRange(List<OccupationalRecommendation> OccupationalRecommendations);
    void Update(OccupationalRecommendation OccupationalRecommendation);
    void Delete(OccupationalRecommendation OccupationalRecommendation);
    void DeleteRange(List<OccupationalRecommendation> OccupationalRecommendations);
}
