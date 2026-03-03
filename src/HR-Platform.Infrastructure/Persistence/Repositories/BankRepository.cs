using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Banks;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class BankRepository(ApplicationDbContext context) : IBankRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(Bank Bank) => _context.Banks.Add(Bank);

        public void Delete(Bank Bank) => _context.Banks.Remove(Bank);
        public void DeleteRange(List<Bank> Banks) => _context.Banks.RemoveRange(Banks);
        public void Update(Bank Bank) => _context.Banks.Update(Bank);

        public async Task<bool> ExistsAsync(BankId id) => await _context.Banks
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<List<Bank>> GetAll() => await _context.Banks
            .Include(x => x.Collaborators)
            .Include(z => z.BankAccounts)
            .AsNoTracking()
            .ToListAsync();

        public async Task<Bank?> GetByIdAsync(BankId Id) =>
            await _context.Banks
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<Bank?> GetNoneBankByCompanyIdAsync(CompanyId companyId) =>
            await _context.Banks
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        public void AddRangeBanks(List<Bank> Banks) => _context.Banks
            .AddRange(Banks);

        public async Task<List<Bank>?> GetByCompanyIdAsync(CompanyId companyId, int page, int pageSize)
        {
            if (page == 0 && pageSize == 0)
            {
                return await _context.Banks.Include(c => c.Collaborators)
                    .Include(z => z.BankAccounts)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();
            }
            else
            {
                return await _context.Banks.Include(c => c.Collaborators)
                    .Include(z => z.BankAccounts)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .Skip((page) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        public async Task<int> GetNumberOfBanks(CompanyId id) {
           List<Bank>? amount = await _context.Banks.Where(p => p.CompanyId == id).ToListAsync();
           return amount.Count - 1;
        }
    }
}
