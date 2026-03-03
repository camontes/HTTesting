using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.OccupationalTests;

public interface IOccupationalTestRepository
{
    Task<OccupationalTest?> GetByIdAsync(OccupationalTestId id);
    Task<List<OccupationalTest>?> GetByCollaboratorIdAsync(CollaboratorId  collaboratorId);
    Task<bool> ExistsAsync(OccupationalTestId id);
    void Add(OccupationalTest OccupationalTest);
    void Add(List<OccupationalTest> OccupationalTests);
    void Update(OccupationalTest OccupationalTest);
    void Delete(OccupationalTest OccupationalTest);
    void DeleteRange(List<OccupationalTest> OccupationalTests);
}
