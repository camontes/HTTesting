using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Risks;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.Risks.Delete;

internal sealed class DeleteRisksCommandHandler(
    IRiskRepository riskRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteRisksCommand, ErrorOr<bool>>
{
    private readonly IRiskRepository _riskRepository = riskRepository ?? throw new ArgumentNullException(nameof(riskRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteRisksCommand query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");

        if (await _riskRepository.GetByIdAsync(new RiskId(query.RiskId)) is not Risk risk)
            return Error.NotFound("Risk.NotFound", "The Risk with the provide Id was not found.");

        try
        {
            _riskRepository.Delete(risk);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}