using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EmergencyPlanTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class EmergencyPlanTypeRepository(ApplicationDbContext context) : IEmergencyPlanTypeRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(EmergencyPlanType EmergencyPlanType) => _context.EmergencyPlanTypes.Add(EmergencyPlanType);
        public void Delete(EmergencyPlanType EmergencyPlanType) => _context.EmergencyPlanTypes.Remove(EmergencyPlanType);
        public void Update(EmergencyPlanType EmergencyPlanType) => _context.EmergencyPlanTypes.Update(EmergencyPlanType);
        public async Task<List<EmergencyPlanType>> GetAll() => await _context.EmergencyPlanTypes
            .AsNoTracking()
            .ToListAsync();

        public async Task<EmergencyPlanType?> GetByIdAsync(EmergencyPlanTypeId Id) =>
            await _context.EmergencyPlanTypes
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<EmergencyPlanType?> GetNoneEmergencyPlanTypeByCompanyIdAsync(CompanyId companyId) =>
            await _context.EmergencyPlanTypes
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();

        public async Task<List<EmergencyPlanType>?> GetByCompanyIdAsync(CompanyId companyId) =>
                 await _context.EmergencyPlanTypes.Where(p => p.CompanyId == companyId && p.Name != "Ninguno").ToListAsync();
    }
}
