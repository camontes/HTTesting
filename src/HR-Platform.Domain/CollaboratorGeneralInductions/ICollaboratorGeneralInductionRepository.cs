using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.CollaboratorGeneralInductions;

public interface ICollaboratorGeneralInductionRepository
{
    Task<CollaboratorGeneralInduction?> GetByIdAsync(CollaboratorGeneralInductionId id);
    Task<List<CollaboratorGeneralInduction>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId);
    Task<List<CollaboratorGeneralInduction>> GetByInductionIdAsync(InductionId inductionId);
    Task<List<CollaboratorGeneralInduction>> GetAllAsync();
    Task<SearchFilter<CollaboratorGeneralInduction>> SearchAsync(BasicRequestSearch request);
    void Add(CollaboratorGeneralInduction CollaboratorGeneralInduction);
    void AddRange(List<CollaboratorGeneralInduction> CollaboratorGeneralInduction);
    void DeleteRange(List<CollaboratorGeneralInduction> CollaboratorGeneralInduction);
    Task DeleteById(CollaboratorGeneralInductionId CollaboratorGeneralInductionId);
    void UpdateRange(List<CollaboratorGeneralInduction> CollaboratorGeneralInduction);
    void Update(CollaboratorGeneralInduction CollaboratorGeneralInduction);
    void Delete(CollaboratorGeneralInduction CollaboratorGeneralInduction);
}
