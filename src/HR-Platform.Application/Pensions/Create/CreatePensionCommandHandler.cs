using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Pensions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Pensions.Create;

internal sealed class CreatePensionsCommandHandler(IPensionRepository PensionRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreatePensionsCommand, ErrorOr<bool>>
{
    private readonly IPensionRepository _PensionRepository = PensionRepository ?? throw new ArgumentNullException(nameof(PensionRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreatePensionsCommand command, CancellationToken cancellationToken)
    {
        string creationDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string editionDateString = creationDateString;

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Pensions.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Pensions.EditionDate", "EditionDate is not valid");


        List<Pension> pensionsToAdd = [];

        foreach (PensionData pensionData in command.PensionsDataList)
        {
            Pension pension = new(
                new PensionId(Guid.NewGuid()),
                new CompanyId(Guid.Parse(pensionData.CompanyId)),
                pensionData.Name,
                pensionData.NameEnglish,
                pensionData.IsEditable,
                pensionData.IsDeleteable,
                creationDate,
                editionDate
            );
            pensionsToAdd.Add(pension);
        }

        try
        {
            _PensionRepository.AddRangePensions(pensionsToAdd);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}