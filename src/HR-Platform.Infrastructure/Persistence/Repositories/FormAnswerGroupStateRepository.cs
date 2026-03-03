using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.FormAnswerGroupStates;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class FormAnswerGroupStateRepository(ApplicationDbContext context) : IFormAnswerGroupStateRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<List<FormAnswerGroupState>> GetAllWithoutNoneAsync() => await _context.FormAnswerGroupStates
            .Where(fags => fags.Name != "Ninguno")
            .AsNoTracking()
            .ToListAsync();
    }
}
