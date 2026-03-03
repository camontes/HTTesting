using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.BankAccounts;

public interface IBankAccountRepository
{
    Task<List<BankAccount>> GetAll();
    Task<BankAccount?> GetByIdAsync(BankAccountId id);
    Task<BankAccount?> GetNoneBankAccountByAccountNumberAsync();
    Task<bool> ExistsAsync(BankAccountId id);
    void AddRangeBankAccounts(List<BankAccount> BankAccount);
    void Add(BankAccount BankAccount);
    void Update(BankAccount BankAccount);
    void Delete(BankAccount BankAccount);
}
