using HR_Platform.Domain.SurveyQuestionTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class SurveyQuestionTypeRepository(ApplicationDbContext context) : ISurveyQuestionTypeRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<List<SurveyQuestionType>> GetAll() => await _context.SurveyQuestionTypes
            .AsNoTracking()
            .ToListAsync();

        public async Task<SurveyQuestionType?> GetByIdAsync(SurveyQuestionTypeId Id) =>
            await _context.SurveyQuestionTypes
            .AsNoTracking()
            .SingleOrDefaultAsync(sqt => sqt.Id == Id);
    }
}
