using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.TypeAccounts;

public interface ITypeAccountRepository
{
    Task<List<TypeAccount>> GetAll();
    Task<List<TypeAccount>> GetGroupByIds(List<TypeAccountId> ids);
    Task<TypeAccount?> GetByIdAsync(TypeAccountId id);
    Task<TypeAccount?> GetNoneTypeAccountByCompanyIdAsync(CompanyId companyId);
    Task<List<TypeAccount>?> GetByCompanyIdAsync(CompanyId CompanyId, int page, int pageSize);
    Task<bool> ExistsAsync(TypeAccountId id);
    Task<int> GetNumberOfTypeAccounts(CompanyId id);
    void AddRangeTypeAccounts(List<TypeAccount> TypeAccount);
    void Add(TypeAccount pension);
    void Update(TypeAccount TypeAccount);
    void Delete(TypeAccount TypeAccount);
    void DeleteRange(List<TypeAccount> TypeAccounts);
}
