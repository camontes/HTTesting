using ErrorOr;
using HR_Platform.Application.Pensions.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Pensions;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Pensions.Update;

internal sealed class UpdatePensionCommandHandler: IRequestHandler<UpdatePensionCommand, ErrorOr<bool>>
{
    private readonly IPensionRepository _pensionRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdatePensionCommandHandler
    (
        IPensionRepository pensionRepository,
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork
    )
    {
        _pensionRepository = pensionRepository ?? throw new ArgumentNullException(nameof(pensionRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<bool>> Handle(UpdatePensionCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(Guid.Parse(query.CompanyId))) is not Company company)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        if (await _pensionRepository.GetByIdAsync(new PensionId(query.Id)) is not Pension oldPension)
        {
            return Error.NotFound("Pension.NotFound", "The pension with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

         if (!string.IsNullOrEmpty(query.Name) && query.Name != oldPension.Name)
        {
            oldPension.Name = query.Name;
            oldPension.EditionDate = editionDate;
            _pensionRepository.Update(oldPension);
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