using HR_Platform.Domain.EmergencyPlans;
using HR_Platform.Domain.EmergencyPlanTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class EmergencyPlanRepository(ApplicationDbContext context) : IEmergencyPlanRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(EmergencyPlan EmergencyPlan) => _context.EmergencyPlans.Add(EmergencyPlan);
        public void Delete(EmergencyPlan EmergencyPlan) => _context.EmergencyPlans.Remove(EmergencyPlan);
        public void Update(EmergencyPlan EmergencyPlan) => _context.EmergencyPlans.Update(EmergencyPlan);
        public async Task<List<EmergencyPlan>> GetAll() => await _context.EmergencyPlans
            .AsNoTracking()
            .ToListAsync();

        public async Task<EmergencyPlan?> GetByIdAsync(EmergencyPlanId Id) =>
            await _context.EmergencyPlans
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<List<EmergencyPlan>> GetEmergencyPlanTypeId(EmergencyPlanTypeId EmergencyPlanTypeId) =>
                 await _context.EmergencyPlans.Where(p => p.EmergencyPlanTypeId == EmergencyPlanTypeId).ToListAsync();
    }
}
