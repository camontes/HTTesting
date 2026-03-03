using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.FamilyCompositions;

public interface IFamilyCompositionRepository
{
    Task<FamilyComposition?> GetByIdAsync(FamilyCompositionId id);
    Task<List<FamilyComposition>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId);
    void Add(FamilyComposition familyComposition);
    void Update(FamilyComposition familyComposition);
    void Delete(FamilyComposition familyComposition);
}
