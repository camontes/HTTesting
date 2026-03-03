using HR_Platform.Domain.Companies;
using HR_Platform.Domain.ContractTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class ContractTypeRepository(ApplicationDbContext context) : IContractTypeRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(ContractType ContractType) => _context.ContractTypes.Add(ContractType);
        public void AddRange(List<ContractType> ContractTypes) => _context.ContractTypes.AddRange(ContractTypes);

        public void Delete(ContractType ContractType) => _context.ContractTypes.Remove(ContractType);
        public void DeleteRange(List<ContractType> ContractTypes) => _context.ContractTypes.RemoveRange(ContractTypes);
        public void Update(ContractType ContractType) => _context.ContractTypes.Update(ContractType);

        public async Task<bool> ExistsAsync(ContractTypeId id) => await _context.ContractTypes
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<List<ContractType>> GetAll() => await _context.ContractTypes
            .AsNoTracking()
            .ToListAsync();

        public async Task<ContractType?> GetByIdAsync(ContractTypeId Id) =>
            await _context.ContractTypes
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<ContractType?> GetNoneContractTypeByCompanyIdAsync(CompanyId companyId) =>
            await _context.ContractTypes
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        public void AddRangeContractTypes(List<ContractType> ContractTypes) => _context.ContractTypes
            .AddRange(ContractTypes);

        public async Task<List<ContractType>?> GetByCompanyIdAsync(CompanyId companyId, int page, int pageSize)
        {
            if (page == 0 && pageSize == 0)
            {
                return await _context.ContractTypes.Include(c => c.CollaboratorContracts)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();
            }
            else
            {
                return await _context.ContractTypes.Include(c => c.CollaboratorContracts)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .Skip((page) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        public async Task<int> GetNumberOfContractTypes(CompanyId id) {
           List<ContractType>? amount =  await _context.ContractTypes.Where(p => p.CompanyId == id).ToListAsync();
           return amount.Count - 1;
        }
    }
}
