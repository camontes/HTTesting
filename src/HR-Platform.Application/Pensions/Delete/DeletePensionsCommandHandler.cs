using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Pensions;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Pensions.Delete;

internal sealed class DeletePensionsCommandHandler(
    IPensionRepository pensionRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeletePensionsCommand, ErrorOr<bool>>
{
    private readonly IPensionRepository _pensionRepository = pensionRepository ?? throw new ArgumentNullException(nameof(pensionRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeletePensionsCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        /* Only 1 Severance Benefit validation */

        List<Pension>? pensions = await _pensionRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), 0, 0);

        if (pensions == null || pensions.Count - query.PensionsList.Count < 1)
            return Error.NotFound("Pension.Count", "The pensions cannot be deleted, there must be at least one");

        /* Match Pension */

        List<Pension>? tpensionsMatched = pensions.Where(x => query.PensionsList.Any(y => new PensionId(y) == x.Id && (x.Collaborators == null || x.Collaborators.Count == 0))).ToList();

        try
        {
            if (tpensionsMatched != null && tpensionsMatched.Count > 0)
            {
                _pensionRepository.DeleteRange(tpensionsMatched);

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