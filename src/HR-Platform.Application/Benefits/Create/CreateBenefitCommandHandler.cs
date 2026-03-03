using ErrorOr;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Benefits;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Benefits.Create;

internal sealed class CreateBenefitsCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICompanyRepository companyRepository,
    IBenefitRepository benefitRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateBenefitsCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IBenefitRepository _benefitRepository = benefitRepository ?? throw new ArgumentNullException(nameof(benefitRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateBenefitsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(command.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Benefits.CreationDate", "CreationDate is not valid");

        if(!command.IsAvailableForAll && command.MinimumMonthsConstraint == 0 && command.MinimumMonthsConstraint > 999)
            return Error.Validation("Benefits.MinimumMonths", "Minimum Months out of range");

        if (!command.IsAvailableForAll && command.IsAnotherContraint && string.IsNullOrEmpty(command.AnotherContraint))
            return Error.Validation("Benefits.AnotherContraint", "Another Contraint can not be empty");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        Benefit result = new
        (
            new BenefitId(Guid.NewGuid()),
            oldCompany.Id,
            command.Name,
            command.Description,
            command.IsAvailableForAll,
            !command.IsAvailableForAll ? command.MinimumMonthsConstraint : 0,
            !string.IsNullOrEmpty(command.AnotherContraint) && command.IsAnotherContraint && !command.IsAvailableForAll ? command.AnotherContraint : string.Empty,
            command.IsAnotherContraint,
            !string.IsNullOrEmpty(command.FileName) ? command.FileName : string.Empty,
            !string.IsNullOrEmpty(command.FileURL) ? command.FileURL: string.Empty,
            creationDate, // creationDateFile
            !string.IsNullOrEmpty(command.ImageName ) ? command.ImageName : string.Empty,
            !string.IsNullOrEmpty(command.ImageURL)? command.ImageURL : string.Empty,
            command.IsAddedButton,
            !string.IsNullOrEmpty(command.ButtonName) && command.IsAddedButton ? command.ButtonName : string.Empty,
            command.IsSurveyInclude,
            command.EmailChangeBy,
            CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty,
            false, //IsVisible
            false, //IsPinned
            true, //IsEditable
            true, //IsDeletable
            creationDate,
            creationDate
       );

        try
        {
            _benefitRepository.Add(result);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}