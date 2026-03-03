using ErrorOr;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Risks;
using HR_Platform.Domain.RiskTypeMains;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Risks.Create;

internal sealed class CreateRisksCommandHandler(
    IRiskTypeMainRepository riskTypeMainRepository,
    ICompanyRepository companyRepository,
    IRiskRepository RiskRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateRisksCommand, ErrorOr<bool>>
{
    private readonly IRiskTypeMainRepository _riskTypeMainRepository = riskTypeMainRepository ?? throw new ArgumentNullException(nameof(riskTypeMainRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IRiskRepository _riskRepository = RiskRepository ?? throw new ArgumentNullException(nameof(RiskRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateRisksCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(command.CompanyId)) is null)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Risks.CreationDate", "CreationDate is not valid");

        if (await _riskTypeMainRepository.GetByIdAsync(new RiskTypeMainId(command.RiskTypeId)) is not RiskTypeMain riskTypeMain)
            return Error.Validation("Risks.RiskTypeMain", "RiskType with the provide Id was not found.");

        Risk result = new
        (
            new RiskId(Guid.NewGuid()),
            riskTypeMain.Id, // RiskTypeMainId
            command.Name, // Name
            command.Description is not null ? command.Description : string.Empty, // Description
            command.ImageFileURL is not null ? command.ImageFileURL : string.Empty, // ImageURL
            command.ImageFileName is not null ? command.ImageFileName : string.Empty, // ImageName
            creationDate, // ImageCreationTime
            command.VideoFileURL is not null ? command.VideoFileURL : string.Empty, // VideoURL
            command.VideoFileName is not null ? command.VideoFileName : string.Empty, // VideoName
            false, // IsVisible
            true,
            true,
            creationDate,
            creationDate
        );

        _riskRepository.Add(result);

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