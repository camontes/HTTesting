using HR_Platform.Domain.CollaboratorGeneralInductions;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class CollaboratorGeneralInductionRepository(ApplicationDbContext context) : ICollaboratorGeneralInductionRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(CollaboratorGeneralInduction CollaboratorGeneralInduction) => _context.Add(CollaboratorGeneralInduction);

    public void AddRange(List<CollaboratorGeneralInduction> CollaboratorGeneralInduction) => _context.AddRange(CollaboratorGeneralInduction);

    public void Delete(CollaboratorGeneralInduction CollaboratorGeneralInduction) => _context.Remove(CollaboratorGeneralInduction);

    public void DeleteRange(List<CollaboratorGeneralInduction> CollaboratorGeneralInduction) => _context.RemoveRange(CollaboratorGeneralInduction);

    public void Update(CollaboratorGeneralInduction CollaboratorGeneralInduction) => _context.Update(CollaboratorGeneralInduction);

    public void UpdateRange(List<CollaboratorGeneralInduction> CollaboratorGeneralInduction) => _context.UpdateRange(CollaboratorGeneralInduction);

    public async Task<List<CollaboratorGeneralInduction>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
        await _context.CollaboratorGeneralInductions.Where(x => x.CollaboratorId == collaboratorId)
        .Include(z => z.Induction).ToListAsync();

    public async Task<CollaboratorGeneralInduction?> GetByIdAsync(CollaboratorGeneralInductionId id) =>
        await _context.CollaboratorGeneralInductions.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);

    public async Task DeleteById(CollaboratorGeneralInductionId collaboratorLifePreferenceId)
    {
        CollaboratorGeneralInduction? collaboratorLifePreference = await _context.CollaboratorGeneralInductions.FindAsync(collaboratorLifePreferenceId);
        if (collaboratorLifePreference != null)
        {
            _context.CollaboratorGeneralInductions.Remove(collaboratorLifePreference);
        }
    }

    public async Task<List<CollaboratorGeneralInduction>> GetByInductionIdAsync(InductionId inductionId) =>
        await _context.CollaboratorGeneralInductions.Include(x => x.Collaborator).Where(r => r.InductionId == inductionId).ToListAsync();

    public async Task<List<CollaboratorGeneralInduction>> GetAllAsync() => 
        await _context.CollaboratorGeneralInductions.Include(x => x.Collaborator).ThenInclude(e => e.Assignation).Include(t => t.Induction).ThenInclude(y => y.InductionFiles).AsNoTracking().ToListAsync();

    public async Task<SearchFilter<CollaboratorGeneralInduction>> SearchAsync(BasicRequestSearch request)
    {
        var filter = _context.CollaboratorGeneralInductions
             .AsNoTracking()
             .Include(cgi => cgi.Collaborator)
             .ThenInclude(c => c.Assignation)
             .Include(cgi => cgi.Induction)
             .ThenInclude(i => i.InductionFiles)
             .Where(cgi => DbFunctions.DbFunctions.RemoveAccents(cgi.Collaborator.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query))).AsEnumerable();

        var baseQuery = filter;

        var totalCount = baseQuery.Count();

        List<CollaboratorGeneralInduction> items = request.Page == 0 || request.PageSize == 0
            ? [.. baseQuery]
            : baseQuery
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

        return new SearchFilter<CollaboratorGeneralInduction>
        {
            TotalCount = totalCount,
            Items = items
        };
    }
}
