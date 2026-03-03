using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.SeveranceBenefits;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.SeveranceBenefits.Delete;

internal sealed class DeleteSeveranceBenefitsCommandHandler(
    ISeveranceBenefitRepository severanceBenefitRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteSeveranceBenefitsCommand, ErrorOr<bool>>
{
    private readonly ISeveranceBenefitRepository _severanceBenefitRepository = severanceBenefitRepository ?? throw new ArgumentNullException(nameof(severanceBenefitRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteSeveranceBenefitsCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");
              
        /* Match Severance Benefits */

        List<SeveranceBenefit>? severanceBenefits = await _severanceBenefitRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), 0, 0);

        List<SeveranceBenefit>? severanceBenefitsMatched = severanceBenefits?.Where(x => query.SeveranceBenefitsList.Any(y => new SeveranceBenefitId(y) == x.Id)
            && (x.Collaborators == null || x.Collaborators.Count == 0)).ToList();

        List<SeveranceBenefit>? severanceBenefitsNotMatched = [];
        if(severanceBenefitsMatched != null && severanceBenefitsMatched.Count > 0)
            severanceBenefitsNotMatched = severanceBenefits?.Except(severanceBenefitsMatched).ToList();

        /* Only 1 Severance Benefit validation */

        if (severanceBenefitsNotMatched == null || severanceBenefitsNotMatched.Count == 0)
            return Error.NotFound("SeveranceBenefits.Count", "The Severance Benefits cannot be deleted");

        try
        {
            if (severanceBenefitsMatched != null && severanceBenefitsMatched.Count > 0)
            {
                _severanceBenefitRepository.DeleteRange(severanceBenefitsMatched);

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