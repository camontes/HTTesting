using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.CollaboratorSoftSkills;

public interface ICollaboratorSoftSkillRepository
{
    Task<CollaboratorSoftSkill?> GetByIdAsync(CollaboratorSoftSkillId id);
    Task<List<CollaboratorSoftSkill>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId);
    void Add(CollaboratorSoftSkill CollaboratorSoftSkill);
    void AddRange(List<CollaboratorSoftSkill> CollaboratorSoftSkill);
    void DeleteRange(List<CollaboratorSoftSkill> CollaboratorSoftSkill);
    Task DeleteById(CollaboratorSoftSkillId CollaboratorSoftSkillId);
    void UpdateRange(List<CollaboratorSoftSkill> CollaboratorSoftSkill);
    void Update(CollaboratorSoftSkill CollaboratorSoftSkill);
    void Delete(CollaboratorSoftSkill CollaboratorSoftSkill);
}
