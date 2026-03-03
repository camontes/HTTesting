using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.CollaboratorTechnologyTools;

public interface ICollaboratorTechnologyToolRepository
{
    Task<CollaboratorTechnologyTool?> GetByIdAsync(CollaboratorTechnologyToolId id);
    Task<List<CollaboratorTechnologyTool>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId);
    void Add(CollaboratorTechnologyTool CollaboratorTecnhologyTool);
    void AddRange(List<CollaboratorTechnologyTool> CollaboratorTecnhologyTool);
    void DeleteRange(List<CollaboratorTechnologyTool> CollaboratorTecnhologyTool);
    Task DeleteById(CollaboratorTechnologyToolId CollaboratorTecnhologyToolId);
    void UpdateRange(List<CollaboratorTechnologyTool> CollaboratorTecnhologyTool);
    void Update(CollaboratorTechnologyTool CollaboratorTecnhologyTool);
    void Delete(CollaboratorTechnologyTool CollaboratorTecnhologyTool);
}
