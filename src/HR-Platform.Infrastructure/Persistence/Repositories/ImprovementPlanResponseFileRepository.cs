using HR_Platform.Domain.ImprovementPlanResponseFiles;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class ImprovementPlanResponseFileRepository(ApplicationDbContext context) : IImprovementPlanResponseFileRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void AddRange(List<ImprovementPlanResponseFile> mprovementPlanResponseFiles) => _context.ImprovementPlanResponseFiles.AddRange(mprovementPlanResponseFiles);
    }
}

