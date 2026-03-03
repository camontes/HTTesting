using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.Contracts;
public interface ICollaboratorContractRepository
{
    Task<CollaboratorContract?> GetByIdAsync(CollaboratorContractId id);
    Task<List<CollaboratorContract>?> GetByCompanyIdAsync(CompanyId companyId, int page, int pageSize);
    Task<bool> ExistsAsync(CollaboratorContractId id);
    Task<CollaboratorContract?> GetNoneCollaboratorContractByCompanyIdAsync(CompanyId companyId);

    void AddRangeContracts(List<CollaboratorContract> contract);
    void Add(CollaboratorContract contract);
    void Update(CollaboratorContract contract);
    void Delete(CollaboratorContract contract);
}
