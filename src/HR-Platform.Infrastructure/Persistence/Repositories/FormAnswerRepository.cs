using HR_Platform.Domain.FormAnswers;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class FormAnswerRepository(ApplicationDbContext context) : IFormAnswerRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(FormAnswer FormAnswer) => _context.FormAnswers.Add(FormAnswer);

        public void Delete(FormAnswer FormAnswer) => _context.FormAnswers.Remove(FormAnswer);

        public void DeleteRange(List<FormAnswer> tags) => _context.FormAnswers.RemoveRange(tags);

        public void Update(FormAnswer FormAnswer) => _context.FormAnswers.Update(FormAnswer);

        public async Task<bool> ExistsAsync(FormAnswerId id) => await _context.FormAnswers
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<List<FormAnswer>> GetAll() => await _context.FormAnswers
            .AsNoTracking()
            .ToListAsync();

        public async Task<FormAnswer?> GetByIdAsync(FormAnswerId Id) =>
            await _context.FormAnswers
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public void AddRangeFormAnswer(List<FormAnswer> FormAnswer) => _context.FormAnswers
            .AddRange(FormAnswer);

        public void AddRange(List<FormAnswer> FormAnswers) => _context.FormAnswers.AddRange(FormAnswers);

        public async Task<SearchFilter<FormAnswer>> GetByCollaboratorIdAndNameSearchAsync(NoveltiesRequestSearch request)
        {
            IQueryable<FormAnswer>? baseQuery = null;

            switch (request.WithResponses)
            {
                case 0:
                    if (string.IsNullOrEmpty(request.CollaboratorName) && string.IsNullOrEmpty(request.FormName))
                    {
                        baseQuery = _context.FormAnswers
                            .AsNoTracking()
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.Assignation)
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.DocumentType)
                            .Include(fa => fa.FormQuestionsType)
                            .ThenInclude(fqt => fqt.Form)
                            .ThenInclude(f => f.NoveltyType)
                            .Include(f => f.FormAnswerGroup)
                            .Where(fa => fa.CollaboratorId == request.CollaboratorId && fa.FormQuestionsType.Form.NoveltyTypeId == request.AreaId);
                    }
                    else if (!string.IsNullOrEmpty(request.CollaboratorName))
                    {
                        baseQuery = _context.FormAnswers
                            .AsNoTracking()
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.Assignation)
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.DocumentType)
                            .Include(fa => fa.FormQuestionsType)
                            .ThenInclude(fqt => fqt.Form)
                            .ThenInclude(f => f.NoveltyType)
                            .Include(f => f.FormAnswerGroup)
                            .Where
                            (
                                fa
                                =>
                                fa.CollaboratorId == request.CollaboratorId
                                &&
                                DbFunctions.DbFunctions.RemoveAccents(fa.Collaborator.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.CollaboratorName.ToLower()))
                                &&
                                fa.FormQuestionsType.Form.NoveltyTypeId == request.AreaId
                            );
                    }
                    else if (!string.IsNullOrEmpty(request.FormName))
                    {
                        baseQuery = _context.FormAnswers
                            .AsNoTracking()
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.Assignation)
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.DocumentType)
                            .Include(fa => fa.FormQuestionsType)
                            .ThenInclude(fqt => fqt.Form)
                            .ThenInclude(f => f.NoveltyType)
                            .Include(f => f.FormAnswerGroup)
                            .Where
                            (
                                fa
                                =>
                                fa.CollaboratorId == request.CollaboratorId
                                &&
                                DbFunctions.DbFunctions.RemoveAccents(fa.FormQuestionsType.Form.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.FormName.ToLower()))
                                &&
                                fa.FormQuestionsType.Form.NoveltyTypeId == request.AreaId
                            );
                    }

                    break;
                case 1:
                    if (string.IsNullOrEmpty(request.CollaboratorName) && string.IsNullOrEmpty(request.FormName))
                    {
                        baseQuery = _context.FormAnswers
                            .AsNoTracking()
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.Assignation)
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.DocumentType)
                            .Include(fa => fa.FormQuestionsType)
                            .ThenInclude(fqt => fqt.Form)
                            .ThenInclude(f => f.NoveltyType)
                            .Include(f => f.FormAnswerGroup)
                            .Where
                            (
                                fa
                                =>
                                fa.CollaboratorId == request.CollaboratorId
                                &&
                                fa.FormQuestionsType.Form.NoveltyTypeId == request.AreaId
                            );
                    }
                    else if(!string.IsNullOrEmpty(request.CollaboratorName))
                    {
                        baseQuery = _context.FormAnswers
                            .AsNoTracking()
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.Assignation)
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.DocumentType)
                            .Include(fa => fa.FormQuestionsType)
                            .ThenInclude(fqt => fqt.Form)
                            .ThenInclude(f => f.NoveltyType)
                            .Include(f => f.FormAnswerGroup)
                            .Where
                            (
                                fa
                                =>
                                fa.CollaboratorId == request.CollaboratorId
                                &&
                                DbFunctions.DbFunctions.RemoveAccents(fa.Collaborator.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.CollaboratorName.ToLower()))
                                &&
                                fa.FormQuestionsType.Form.NoveltyTypeId == request.AreaId
                            );
                    }
                    else if (!string.IsNullOrEmpty(request.FormName))
                    {
                        baseQuery = _context.FormAnswers
                            .AsNoTracking()
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.Assignation)
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.DocumentType)
                            .Include(fa => fa.FormQuestionsType)
                            .ThenInclude(fqt => fqt.Form)
                            .ThenInclude(f => f.NoveltyType)
                            .Include(f => f.FormAnswerGroup)
                            .Where
                            (
                                fa
                                =>
                                fa.CollaboratorId == request.CollaboratorId
                                &&
                                DbFunctions.DbFunctions.RemoveAccents(fa.FormQuestionsType.Form.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.FormName.ToLower()))
                                &&
                                fa.FormQuestionsType.Form.NoveltyTypeId == request.AreaId
                            );
                    }

                    break;
                case 2:
                    if (string.IsNullOrEmpty(request.CollaboratorName) && string.IsNullOrEmpty(request.FormName))
                    {
                        baseQuery = _context.FormAnswers
                            .AsNoTracking()
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.Assignation)
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.DocumentType)
                            .Include(fa => fa.FormQuestionsType)
                            .ThenInclude(fqt => fqt.Form)
                            .ThenInclude(f => f.NoveltyType)
                            .Include(f => f.FormAnswerGroup)
                            .Where
                            (
                                fa
                                =>
                                fa.CollaboratorId == request.CollaboratorId
                                &&
                                fa.FormQuestionsType.Form.NoveltyTypeId == request.AreaId
                            );
                    }
                    else if (!string.IsNullOrEmpty(request.CollaboratorName))
                    {
                        baseQuery = _context.FormAnswers
                            .AsNoTracking()
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.Assignation)
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.DocumentType)
                            .Include(fa => fa.FormQuestionsType)
                            .ThenInclude(fqt => fqt.Form)
                            .ThenInclude(f => f.NoveltyType)
                            .Include(f => f.FormAnswerGroup)
                            .Where
                            (
                                fa
                                =>
                                fa.CollaboratorId == request.CollaboratorId
                                &&
                                DbFunctions.DbFunctions.RemoveAccents(fa.Collaborator.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.CollaboratorName.ToLower()))
                                &&
                                fa.FormQuestionsType.Form.NoveltyTypeId == request.AreaId
                            );
                    }
                    else if (!string.IsNullOrEmpty(request.FormName))
                    {
                        baseQuery = _context.FormAnswers
                            .AsNoTracking()
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.Assignation)
                            .Include(fa => fa.Collaborator)
                            .ThenInclude(c => c.DocumentType)
                            .Include(fa => fa.FormQuestionsType)
                            .ThenInclude(fqt => fqt.Form)
                            .ThenInclude(f => f.NoveltyType)
                            .Include(f => f.FormAnswerGroup)
                            .Where
                            (
                                fa
                                =>
                                fa.CollaboratorId == request.CollaboratorId
                                &&
                                DbFunctions.DbFunctions.RemoveAccents(fa.FormQuestionsType.Form.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.FormName.ToLower()))
                                &&
                                fa.FormQuestionsType.Form.NoveltyTypeId == request.AreaId
                            );
                    }

                    break;
                default:
                    break;
            };

            int totalCount = await baseQuery.CountAsync();
            List<FormAnswer> items = request.Page == 0 || request.PageSize == 0
                ? [.. baseQuery]
                : await baseQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

            return new SearchFilter<FormAnswer>
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<SearchFilter<FormAnswer>> GetByCompanyIdAndNameSearchAsync(NoveltiesRequestSearch request)
        {
            IQueryable<FormAnswer> baseQuery;

            if (string.IsNullOrEmpty(request.CollaboratorName))
            {
                baseQuery = _context.FormAnswers
                    .AsNoTracking()
                    .Include(fa => fa.Collaborator)
                    .ThenInclude(c => c.Assignation)
                    .Include(fa => fa.Collaborator)
                    .ThenInclude(c => c.DocumentType)
                    .Include(fa => fa.FormQuestionsType)
                    .ThenInclude(fqt => fqt.Form)
                    .ThenInclude(f => f.NoveltyType)
                    .Include(f => f.FormAnswerGroup)
                    .Where(fa => fa.Collaborator.CompanyId == request.CompanyId && fa.FormQuestionsType.Form.NoveltyTypeId == request.AreaId);
            }
            else
            {
                baseQuery = _context.FormAnswers
                    .AsNoTracking()
                    .Include(fa => fa.Collaborator)
                    .ThenInclude(c => c.Assignation)
                    .Include(fa => fa.Collaborator)
                    .ThenInclude(c => c.DocumentType)
                    .Include(fa => fa.FormQuestionsType)
                    .ThenInclude(fqt => fqt.Form)
                    .ThenInclude(f => f.NoveltyType)
                    .Include(f => f.FormAnswerGroup)
                    .Where
                    (
                        x
                        =>
                        x.Collaborator.CompanyId == request.CompanyId
                        &&
                        DbFunctions.DbFunctions.RemoveAccents(x.Collaborator.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.CollaboratorName))
                        &&
                        x.FormQuestionsType.Form.NoveltyTypeId == request.AreaId
                    );
            }

            int totalCount = await baseQuery.CountAsync();
            List<FormAnswer> items = request.Page == 0 || request.PageSize == 0
                ? [.. baseQuery]
                : await baseQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

            return new SearchFilter<FormAnswer>
            {
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
