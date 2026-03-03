using HR_Platform.Domain.Companies;
using HR_Platform.Domain.TypeAccounts;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class TypeAccountRepository(ApplicationDbContext context) : ITypeAccountRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(TypeAccount TypeAccount) => _context.TypeAccounts.Add(TypeAccount);

        public void Delete(TypeAccount TypeAccount) => _context.TypeAccounts.Remove(TypeAccount);
        public void DeleteRange(List<TypeAccount> TypeAccounts) => _context.TypeAccounts.RemoveRange(TypeAccounts);
        public void Update(TypeAccount TypeAccount) => _context.TypeAccounts.Update(TypeAccount);

        public async Task<bool> ExistsAsync(TypeAccountId id) => await _context.TypeAccounts
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<List<TypeAccount>> GetAll() => await _context.TypeAccounts
            .AsNoTracking()
            .ToListAsync();

        public async Task<List<TypeAccount>> GetGroupByIds(List<TypeAccountId> ids) => await _context.TypeAccounts
        .AsNoTracking()
            .Include(z => z.BankAccounts)
            .Include(x => x.Collaborators)
            .Where(tc => ids.Contains(tc.Id) && tc.Name != "Ninguno")
            .ToListAsync();

        public async Task<TypeAccount?> GetByIdAsync(TypeAccountId Id) =>
            await _context.TypeAccounts
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<TypeAccount?> GetNoneTypeAccountByCompanyIdAsync(CompanyId companyId) =>
            await _context.TypeAccounts
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        public void AddRangeTypeAccounts(List<TypeAccount> TypeAccounts) => _context.TypeAccounts
            .AddRange(TypeAccounts);

        public async Task<List<TypeAccount>?> GetByCompanyIdAsync(CompanyId companyId, int page, int pageSize)
        {
            if (page == 0 && pageSize == 0)
            {
                return await _context.TypeAccounts
                    .Include(c => c.Collaborators)
                    .Include(z => z.BankAccounts)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();
            }
            else
            {
                return await _context.TypeAccounts
                    .Include(c => c.Collaborators)
                    .Include(z => z.BankAccounts)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .Skip((page) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        public async Task<int> GetNumberOfTypeAccounts(CompanyId id) {
           List<TypeAccount>? amount =  await _context.TypeAccounts.Where(p => p.CompanyId == id).ToListAsync();
           return amount.Count - 1;
        }
    }
}
