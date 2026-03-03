using ErrorOr;
using HR_Platform.Application.ContractTypes.Update;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Risks;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Risks.Update;

internal sealed class UpdateRisksCommandHandler(
    IRiskRepository riskRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateRisksCommand, ErrorOr<bool>>
{
    private readonly IRiskRepository _riskRepository = riskRepository ?? throw new ArgumentNullException(nameof(riskRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateRisksCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");
        int changesConter = 0;

        if (await _companyRepository.GetByIdAsync(new CompanyId(command.CompanyId)) is null)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Risks.EditionDate", "EditionDate is not valid");

        if (await _riskRepository.GetByIdAsync(new RiskId(command.RiskId)) is not Risk risk)
            return Error.Validation("Risks.Risk", "Risk with the provide Id was not found.");

        if (!string.IsNullOrEmpty(command.Name))
        {
            risk.Name = command.Name;
            changesConter += 1;
        }

        if (command.Description is not null && command.Description != risk.Description)
        {
            risk.Description = command.Description;
            changesConter += 1;
        }

        if (risk.ImageURL != command.ImageFileURL && command.IsUpdateImageFile)
        {
            risk.ImageURL = command.ImageFileURL is not null ? command.ImageFileURL : string.Empty;
            risk.ImageCreationTime = editionDate;
            changesConter += 1;
        }

        if (risk.ImageName != command.ImageFileName && command.IsUpdateImageFile)
        {
            risk.ImageName = command.ImageFileName is not null ? command.ImageFileName : string.Empty;
            risk.ImageCreationTime = editionDate;
            changesConter += 1;
        }

        if (risk.VideoURL != command.VideoFileURL && command.IsUpdateVideoFile)
        {
            risk.VideoURL = command.VideoFileURL is not null ? command.VideoFileURL : string.Empty;
            changesConter += 1;
        }

        if (risk.VideoName != command.VideoFileName && command.IsUpdateVideoFile)
        {
            risk.VideoName = command.VideoFileName is not null ? command.VideoFileName : string.Empty;
            changesConter += 1;
        }

        try
        {
            if (changesConter > 0)
            {
                risk.EditionDate = editionDate;
                _riskRepository.Update(risk);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}