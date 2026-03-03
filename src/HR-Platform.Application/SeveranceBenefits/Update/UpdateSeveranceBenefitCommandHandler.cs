using ErrorOr;
using HR_Platform.Application.SeveranceBenefits.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.SeveranceBenefits;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.SeveranceBenefits.Update;

internal sealed class UpdateSeveranceBenefitCommandHandler : IRequestHandler<UpdateSeveranceBenefitCommand, ErrorOr<bool>>
{
    private readonly ISeveranceBenefitRepository _severanceBenefitRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSeveranceBenefitCommandHandler
    (
        ISeveranceBenefitRepository severanceBenefitRepository,
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _severanceBenefitRepository = severanceBenefitRepository ?? throw new ArgumentNullException(nameof(severanceBenefitRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<bool>> Handle(UpdateSeveranceBenefitCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(Guid.Parse(query.CompanyId))) is not Company company)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        if (await _severanceBenefitRepository.GetByIdAsync(new SeveranceBenefitId(query.Id)) is not SeveranceBenefit oldSeveranceBenefit)
        {
            return Error.NotFound("SeveranceBenefit.NotFound", "The severanceBenefit with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

         if (!string.IsNullOrEmpty(query.Name) && query.Name != oldSeveranceBenefit.Name)
        {
            oldSeveranceBenefit.Name = query.Name;
            oldSeveranceBenefit.EditionDate = editionDate;
            _severanceBenefitRepository.Update(oldSeveranceBenefit);
        }

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;

        }
        catch (Exception)
        {
            return false;
        }
    }
}