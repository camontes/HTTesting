using HR_Platform.Domain.ImprovementPlanResponses;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class ImprovementPlanResponseRepository(ApplicationDbContext context) : IImprovementPlanResponseRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(ImprovementPlanResponse improvementPlanResponse) => _context.ImprovementPlanResponses.Add(improvementPlanResponse);
    }
}


