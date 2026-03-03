using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.BrigadeMembers;

public interface IBrigadeMemberRepository
{
    Task<BrigadeMember?> GetByIdAsync(BrigadeMemberId id);
    Task<List<BrigadeMember>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId);
    Task<List<BrigadeMember>> GetByBrigadeAdjustmentIdAsync(BrigadeAdjustmentId brigadeAdjustmentId);
    Task<List<BrigadeMember>> GetAll();
    void Add(BrigadeMember BrigadeMember);
    void AddRange(List<BrigadeMember> BrigadeMember);
    void DeleteRange(List<BrigadeMember> BrigadeMember);
    Task DeleteById(BrigadeMemberId BrigadeMemberId);
    void UpdateRange(List<BrigadeMember> BrigadeMember);
    void Update(BrigadeMember BrigadeMember);
    void Delete(BrigadeMember BrigadeMember);
}
