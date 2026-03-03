using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.WorkplaceInformations;

public interface IWorkplaceInformationRepository
{
    Task<WorkplaceInformation?> GetByIdAsync(WorkplaceInformationId id);
    Task<List<WorkplaceInformation>?> GetByCollaboratorIdAsync(CollaboratorId  collaboratorId);
    Task<bool> ExistsAsync(WorkplaceInformationId id);
    void Add(WorkplaceInformation WorkplaceInformation);
    void AddRange(List<WorkplaceInformation> WorkplaceInformations);
    void Update(WorkplaceInformation WorkplaceInformation);
    void Delete(WorkplaceInformation WorkplaceInformation);
    void DeleteRange(List<WorkplaceInformation> WorkplaceInformations);
}
