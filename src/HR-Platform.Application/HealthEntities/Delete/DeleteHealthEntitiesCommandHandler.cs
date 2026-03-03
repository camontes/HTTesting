using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.HealthEntities;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.HealthEntities.Delete;

internal sealed class DeleteHealthEntitiesCommandHandler(
    IHealthEntityRepository healthEntityRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteHealthEntitiesCommand, ErrorOr<bool>>
{
    private readonly IHealthEntityRepository _healthEntityRepository = healthEntityRepository ?? throw new ArgumentNullException(nameof(healthEntityRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteHealthEntitiesCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        /* Only 1 Severance Benefit validation */

        List<HealthEntity>? healthEntitys = await _healthEntityRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), 0, 0);

        /* Match HealthEntity */
        List<HealthEntity>? thealthEntitysMatched = healthEntitys?.Where(x => query.HealthEntitiesList.Any(y => new HealthEntityId(y) == x.Id && (x.Collaborators == null || x.Collaborators.Count == 0))).ToList();

        try
        {
            if (thealthEntitysMatched != null && thealthEntitysMatched.Count > 0)
            {
                _healthEntityRepository.DeleteRange(thealthEntitysMatched);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}