using HR_Platform.Domain.SurveyQuestionMandatoryTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class SurveyQuestionMandatoryTypeRepository(ApplicationDbContext context) : ISurveyQuestionMandatoryTypeRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<List<SurveyQuestionMandatoryType>> GetAll() => await _context.SurveyQuestionMandatoryTypes
            .AsNoTracking()
            .ToListAsync();

        public async Task<SurveyQuestionMandatoryType?> GetByIdAsync(SurveyQuestionMandatoryTypeId Id) =>
            await _context.SurveyQuestionMandatoryTypes
            .AsNoTracking()
            .SingleOrDefaultAsync(smt => smt.Id == Id);
    }
}
