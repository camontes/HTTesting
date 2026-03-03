using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Forms;
using HR_Platform.Domain.Regulations;
using HR_Platform.Domain.SearchFilters;
using HR_Platform.Domain.Surveys;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class SurveyRepository(ApplicationDbContext context) : ISurveyRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(Survey survey) => _context.Surveys.Add(survey);
        public void AddRange(List<Survey> surveys) => _context.Surveys.AddRange(surveys);

        public void Delete(Survey survey) => _context.Surveys.Remove(survey);
        public void Update(Survey survey) => _context.Surveys.Update(survey);

        public async Task<bool> ExistsAsync(SurveyId id) => await _context.Surveys
            .AsNoTracking()
            .AnyAsync(s => s.Id == id);

        public async Task<Survey?> GetByIdAsync(SurveyId Id) =>
            await _context.Surveys
            .Include(s => s.SurveyQuestions)
            .ThenInclude(sq => sq.SurveyQuestionType)
            .Include(s => s.SurveyQuestions)
            .ThenInclude(sq => sq.SurveyQuestionMandatoryType)
            .AsNoTracking()
            .SingleOrDefaultAsync(s => s.Id == Id);

        public async Task<List<Survey>?> GetByCompanyIdAsync(CompanyId companyId) =>
            await _context.Surveys.Where(s => s.CompanyId == companyId).ToListAsync();

        public async Task<List<Survey>?> GetByAreaAndCompanyIdAsync(AreaId areaId, CompanyId companyId) =>
            await _context.Surveys.Where(s => s.SurveyTypeId == areaId && s.CompanyId == companyId).ToListAsync();

        public async Task<SearchFilter<Survey>> SearchAsync(BasicRequestSearch request)
        {
            var filter = _context.Surveys
            .Where
            (
            s
            =>
                s.SurveyTypeId == request.AreaId
                &&
                s.CompanyId == request.CompanyId
                &&
                DbFunctions.DbFunctions.RemoveAccents(s.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query))
            ).AsEnumerable();

            var baseQuery = filter;

            var totalCount = baseQuery.Count();

            List<Survey> items = request.Page == 0 || request.PageSize == 0
            ? [.. baseQuery]
            : baseQuery
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

            return new SearchFilter<Survey>
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<Survey?> GetByIdWithoutIncludesAsync(SurveyId Id) =>
            await _context.Surveys
            .AsNoTracking()
            .SingleOrDefaultAsync(s => s.Id == Id);
    }
}
