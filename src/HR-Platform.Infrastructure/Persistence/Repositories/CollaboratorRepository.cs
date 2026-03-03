using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Positions;
using HR_Platform.Domain.Roles;
using HR_Platform.Domain.SearchFilters;
using HR_Platform.Domain.ValueObjects;
using HR_Platform.Infrastructure.Persistence.DbFunctions;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class CollaboratorRepository(ApplicationDbContext context) : ICollaboratorRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(Collaborator collaborator) => _context.Collaborators.Add(collaborator);

    public void Delete(Collaborator collaborator) => _context.Collaborators.Remove(collaborator);

    public void Update(Collaborator collaborator) => _context.Collaborators.Update(collaborator);
    public void UpdateRange(List<Collaborator> collaborators) => _context.Collaborators.UpdateRange(collaborators);

    public async Task<bool> ExistsAsync(CollaboratorId id) => await _context.Collaborators.AsNoTracking().AnyAsync(c => c.Id == id);

    public async Task<Collaborator?> GetByIdAsync(CollaboratorId id) => await _context.Collaborators
        .Include(c => c.Assignation)
        .Include(c => c.AssignationType)
        .Include(c => c.BenefitClaimAnswers)
        .Include(c => c.DocumentType)
        .Include(c => c.Position)
        .SingleOrDefaultAsync(c => c.Id == id);

    public async Task<Collaborator?> GetByEmailAsync(string email) => await _context.Collaborators
        .Include(c => c.DocumentType)
        .Include(c => c.Position)
        .Include(c => c.Role)
        .AsNoTracking()
        .SingleOrDefaultAsync(c => c.BusinessEmail == Email.Create(email) || c.PersonalEmail == Email.Create(email));

    public async Task<Collaborator?> GetByCompanyAndRoleIdAsync(CompanyId companyId, RoleId roleId) =>
        await _context.Collaborators.AsNoTracking().SingleOrDefaultAsync(c => c.CompanyId == companyId && c.RoleId == roleId);

    public async Task<List<Collaborator>?> GetByCompanyIdAndIsPendingInvitation(CompanyId companyId, int isPendingInvitation, int page, int pageSize)
    {
        List<Collaborator> collaborators = [];

        if (isPendingInvitation == 3) // Both
        {
            collaborators = await _context.Collaborators
            .Where(c => c.CompanyId == companyId)
            .Include(c => c.Assignation)
            .Include(c => c.DocumentType)
            .Include(c => c.Role)
            .AsNoTracking()
            .Where(c => c.Role.NameEnglish != "Superadministrator")
            .OrderByDescending(c => c.EntranceDate)
            .ThenByDescending(c => c.CreationDate)
            .ToListAsync();
        }
        else
        {
            bool isPendingInvitationBoolean = false;

            if (isPendingInvitation == 1) // True
                isPendingInvitationBoolean = true;

            collaborators = await _context.Collaborators // True or false
            .Where(c => c.CompanyId == companyId && c.IsPendingInvitation == isPendingInvitationBoolean)
            .Include(c => c.Assignation)
            .Include(c => c.DocumentType)
            .Include(c => c.Role)
            .AsNoTracking()
            .Where(c => c.Role.NameEnglish != "Superadministrator")
            .OrderByDescending(c => c.EntranceDate)
            .ThenByDescending(c => c.CreationDate)
            .ToListAsync();
        }

        if (pageSize == 0)
        {
            return collaborators;
        }

        return collaborators
            .Skip((page) * pageSize).Take(pageSize)
            .ToList();
    }

    public async Task<int> GetCountByCompanyIdAndIsPendingInvitation(CompanyId id, int isPendingInvitation)
    {
        int collaboratorsCount = 0;

        if (isPendingInvitation == 3) // Both
        {
            collaboratorsCount = await _context.Collaborators.AsNoTracking().Include(c => c.Role).CountAsync(c => c.CompanyId == id && c.Role.NameEnglish != "Superadministrator");
        }
        else
        {
            bool isPendingInvitationBoolean = false;

            if (isPendingInvitation == 1) // True
                isPendingInvitationBoolean = true;

            collaboratorsCount = await _context.Collaborators.AsNoTracking().Include(c => c.Role).CountAsync(c => c.CompanyId == id && c.IsPendingInvitation == isPendingInvitationBoolean && c.Role.NameEnglish != "Superadministrator");
        }

        return collaboratorsCount;
    }

    public async Task<List<Collaborator>> GetAllByFilter(int page, int pageSize)
    {
        if (page == 0 && pageSize == 0)
        {
            return await _context.Collaborators.Include(c => c.Assignation)
                .Include(c => c.Role)
                .Where(c => c.Role.NameEnglish != "Superadministrator")
                .Include(c => c.DocumentType).ToListAsync();
        }
        else
        {
            return await _context.Collaborators
             .Include(c => c.Assignation)
             .Include(c => c.Role)
             .Where(c => c.Role.NameEnglish != "Superadministrator")
             .Include(c => c.DocumentType).Skip((page) * pageSize).Take(pageSize).ToListAsync();
        }
    }

    public async Task<List<Collaborator>> GetAll() => await _context
        .Collaborators
        .Include(c => c.DocumentType)
        .Include(c => c.Assignation)
        .Include(y => y.Position)
        .Include(c => c.Role)
        .Where(c => c.Role.NameEnglish != "Superadministrator")
        .AsNoTracking()
        .ToListAsync();

    public async Task<List<Collaborator>> GetAllByCompanyId(CompanyId companyId) => await _context
       .Collaborators.
        Where(x => x.CompanyId == companyId)
       .AsNoTracking()
       .ToListAsync();

    public async Task<List<Collaborator>> GetAllCopasst() => await _context
        .Collaborators
        .Include(c => c.Role)
        .Where(c => c.Role.NameEnglish != "Superadministrator")
        .Include(y => y.Position)
        .ToListAsync();

    public async Task<List<Collaborator>> GetAllCollaboratorsWithEvaluationsInHistorical(CompanyId companyId)
        =>
        await _context.Collaborators
        .Include(c => c.DocumentType)
        .Include(c => c.Position)
        .Include(c => c.CollaboratorCriterias)
        .ThenInclude(cc => cc.Evaluator)
        .Include(c => c.CollaboratorCriterias)
        .ThenInclude(cc => cc.CollaboratorCriteriaAnswers)
        .ThenInclude(cca => cca.ImprovementPlans)
        .Where
        (
            c
            =>
            c.CollaboratorCriterias
            .Any
            (
                cc
                =>
                cc.CollaboratorEvaluatedId == c.Id
                &&
                cc.CollaboratorCriteriaAnswers
                .Any
                (
                    cca
                    =>
                    cca.IsInHistorical
                    &&
                    cca.CollaboratorCriteriaId == cc.Id
                )
            )
            &&
            c.CompanyId == companyId
        )
        .ToListAsync();

    public async Task<SearchFilter<Collaborator>> SearchAsyncWithoutPages(BasicRequestSearch request)
    {
        IQueryable<Collaborator> baseQuery;

        baseQuery = _context.Collaborators
            .Include(c => c.Assignation)
            .Include(p => p.Position)
            .Include(a => a.AssignationType)
            .Include(d => d.DocumentType)
            .Include(c => c.Role)
            .Where(c => DbFunctions.DbFunctions.RemoveAccents(c.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query)));


        var totalCount = await baseQuery.CountAsync();
        List<Collaborator> items = baseQuery.ToList();

        return new SearchFilter<Collaborator>
        {
            TotalCount = totalCount,
            Items = items
        };
    }

    public async Task<SearchFilter<Collaborator>> SearchAsync(BasicRequestSearch request)
    {
        bool IsPendingInvitation = request.IsPendingInvitation is not null;
        IQueryable<Collaborator> baseQuery;

        if (IsPendingInvitation)
        {
            baseQuery = _context.Collaborators
            .Include(c => c.Assignation)
            .Include(p => p.Position)
            .Include(a => a.AssignationType)
            .Include(d => d.DocumentType)
            .Include(c => c.Role)
            .Where(c => DbFunctions.DbFunctions.RemoveAccents(c.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query)) && c.IsPendingInvitation);
        }
        else
        {
            baseQuery = _context.Collaborators
            .Include(c => c.Assignation)
            .Include(p => p.Position)
            .Include(a => a.AssignationType)
            .Include(d => d.DocumentType)
            .Include(c => c.Role)
            .Where(c => DbFunctions.DbFunctions.RemoveAccents(c.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query)));
        }


        var totalCount = await baseQuery.CountAsync();
        List<Collaborator> items = request.Page == 0 || request.PageSize == 0
            ? [.. baseQuery]
            : await baseQuery
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

        return new SearchFilter<Collaborator>
        {
            TotalCount = totalCount,
            Items = items
        };
    }

    public async Task<List<Collaborator>> GetByPositionId(PositionId positionId) => await _context
      .Collaborators
      .Include(c => c.Role)
      .Include(y => y.Position)
      .Where(c => c.Role.NameEnglish != "Superadministrator" && c.PositionId == positionId)
      .ToListAsync();
}