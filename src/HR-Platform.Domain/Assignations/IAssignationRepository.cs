using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.Assignations;

public interface IAssignationRepository
{
    Task<List<Assignation>> GetAll();
    Task<Assignation?> GetByIdAsync(AssignationId id);
    Task<List<Assignation>?> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<List<Assignation>?> GetByCompanyIdAndInternalOrExternalAsync(CompanyId CompanyId, bool IsInternalAssignation);
    Task<List<Assignation>> GetDefaultsAsync();
    Task<bool> ExistsAsync(AssignationId id);
    void Add(Assignation assignation);
    void Update(Assignation assignation);
    void Delete(Assignation assignation);
    void DeleteRange(List<Assignation> assignation);
}
