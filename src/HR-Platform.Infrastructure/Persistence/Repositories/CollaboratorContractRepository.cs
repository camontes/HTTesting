using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class CollaboratorContractRepository(ApplicationDbContext context) : ICollaboratorContractRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(CollaboratorContract CollaboratorContract) => _context.CollaboratorContracts.Add(CollaboratorContract);

        public void Delete(CollaboratorContract CollaboratorContract) => _context.CollaboratorContracts.Remove(CollaboratorContract);
        public void Update(CollaboratorContract CollaboratorContract) => _context.CollaboratorContracts.Update(CollaboratorContract);

        public async Task<bool> ExistsAsync(CollaboratorContractId id) => await _context.CollaboratorContracts
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<CollaboratorContract?> GetByIdAsync(CollaboratorContractId Id) =>
            await _context.CollaboratorContracts
            .AsNoTracking()
            .Include(c => c.ContractTypes)
            .Include(m => m.DefaultCurrencyTypes)
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<List<CollaboratorContract>?> GetByCompanyIdAsync(CompanyId companyId, int page, int pageSize)
        {
            if (page == 0 && pageSize == 0)
            {
                return await _context.CollaboratorContracts.Include(c => c.Collaborators)
                    .Where(h => h.CompanyId == companyId)
                    .ToListAsync();
            }
            else
            {
                return await _context.CollaboratorContracts.Include(c => c.Collaborators)
                    .Where(h => h.CompanyId == companyId)
                    .Skip((page) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        public async Task<int> GetNumberOfCollaboratorContracts(CompanyId id)
        {
            List<CollaboratorContract>? amount = await _context.CollaboratorContracts.Where(p => p.CompanyId == id).ToListAsync();
            return amount.Count - 1;
        }

        public void AddRangeContracts(List<CollaboratorContract> contract) =>
             _context.CollaboratorContracts.AddRange(contract);


        public async Task<CollaboratorContract?> GetNoneCollaboratorContractByCompanyIdAsync(CompanyId companyId) =>
           await _context.CollaboratorContracts
           .Where(p => p.CompanyId == companyId && p.Arl == "Ninguno" && p.Bonus == "Ninguno")
           .FirstOrDefaultAsync();
    }
}
