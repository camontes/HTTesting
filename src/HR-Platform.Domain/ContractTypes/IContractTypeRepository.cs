using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.ContractTypes;

public interface IContractTypeRepository
{
    Task<List<ContractType>> GetAll();
    Task<ContractType?> GetByIdAsync(ContractTypeId id);
    Task<ContractType?> GetNoneContractTypeByCompanyIdAsync(CompanyId companyId);
    Task<List<ContractType>?> GetByCompanyIdAsync(CompanyId CompanyId, int page, int pageSize);
    Task<bool> ExistsAsync(ContractTypeId id);
    Task<int> GetNumberOfContractTypes(CompanyId id);
    void AddRangeContractTypes(List<ContractType> ContractType);
    void Add(ContractType ContractType);
    void AddRange(List<ContractType> ContractTypes);
    void Update(ContractType ContractType);
    void Delete(ContractType ContractType);
    void DeleteRange(List<ContractType> ContractTypes);
}
