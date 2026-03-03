using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Roles;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.Collaborators;

public interface ICollaboratorRepository : ISearchFilterRepository<Collaborator>
{
    Task<List<Collaborator>> GetAll();
    Task<List<Collaborator>> GetAllByCompanyId(CompanyId companyId);
    Task<List<Collaborator>> GetAllCopasst();
    Task<List<Collaborator>> GetAllCollaboratorsWithEvaluationsInHistorical(CompanyId companyId);
    Task<SearchFilter<Collaborator>> SearchAsyncWithoutPages(BasicRequestSearch request);
    Task<List<Collaborator>> GetByPositionId(PositionId positionId);
    Task<List<Collaborator>> GetAllByFilter(int page, int pageSize);
    Task<Collaborator?> GetByIdAsync(CollaboratorId id);
    Task<Collaborator?> GetByEmailAsync(string email);
    Task<Collaborator?> GetByCompanyAndRoleIdAsync(CompanyId companyId, RoleId roleId);
    Task<List<Collaborator>?> GetByCompanyIdAndIsPendingInvitation(CompanyId companyId, int isPendingInvitation, int page, int pageSize);
    Task<int> GetCountByCompanyIdAndIsPendingInvitation(CompanyId id, int isPendingInvitation);
    Task<bool> ExistsAsync(CollaboratorId id);
    void Add(Collaborator collaborator);
    void Update(Collaborator collaborator);
    void UpdateRange(List<Collaborator> collaborators);
    void Delete(Collaborator collaborator);
}
