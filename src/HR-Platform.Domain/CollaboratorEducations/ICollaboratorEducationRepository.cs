using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.CollaboratorEducations;

public interface ICollaboratorEducationRepository
{
    Task<CollaboratorEducation?> GetByIdAsync(CollaboratorEducationId id);
    Task<List<CollaboratorEducation>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId);
    void Add(CollaboratorEducation CollaboratorEducation);
    void AddRange(List<CollaboratorEducation> CollaboratorEducation);
    void DeleteRange(List<CollaboratorEducation> CollaboratorEducation);
    Task DeleteById(CollaboratorEducationId CollaboratorEducationId);
    void UpdateRange(List<CollaboratorEducation> CollaboratorEducation);
    void Update(CollaboratorEducation CollaboratorEducation);
    void Delete(CollaboratorEducation CollaboratorEducation);
}
