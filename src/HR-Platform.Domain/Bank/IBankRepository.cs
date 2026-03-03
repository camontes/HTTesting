using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.Banks;

public interface IBankRepository
{
    Task<List<Bank>> GetAll();
    Task<Bank?> GetByIdAsync(BankId id);
    Task<Bank?> GetNoneBankByCompanyIdAsync(CompanyId companyId);
    Task<List<Bank>?> GetByCompanyIdAsync(CompanyId CompanyId, int page, int pageSize);
    Task<bool> ExistsAsync(BankId id);
    Task<int> GetNumberOfBanks(CompanyId id);
    void AddRangeBanks(List<Bank> Bank);
    void Add(Bank Bank);
    void Update(Bank Bank);
    void Delete(Bank Bank);
    void DeleteRange(List<Bank> Banks);
}
