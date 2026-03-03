using HR_Platform.Domain.Companies;
using HR_Platform.Domain.BankAccounts;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class BankAccountRepository(ApplicationDbContext context) : IBankAccountRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(BankAccount BankAccount) => _context.BankAccounts.Add(BankAccount);

        public void Delete(BankAccount BankAccount) => _context.BankAccounts.Remove(BankAccount);
        public void Update(BankAccount BankAccount) => _context.BankAccounts.Update(BankAccount);

        public async Task<bool> ExistsAsync(BankAccountId id) => await _context.BankAccounts
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<List<BankAccount>> GetAll() => await _context.BankAccounts
            .AsNoTracking()
            .ToListAsync();

        public async Task<BankAccount?> GetByIdAsync(BankAccountId Id) =>
            await _context.BankAccounts
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<BankAccount?> GetNoneBankAccountByAccountNumberAsync() =>
            await _context.BankAccounts
            .Where(p => p.AccountNumber == "")
            .FirstOrDefaultAsync();
        public void AddRangeBankAccounts(List<BankAccount> BankAccounts) => _context.BankAccounts
            .AddRange(BankAccounts);

    }
}
