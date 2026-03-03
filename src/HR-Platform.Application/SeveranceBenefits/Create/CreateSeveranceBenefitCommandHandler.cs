using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.SeveranceBenefits;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.SeveranceBenefits.Create;

internal sealed class CreateSeveranceBenefitsCommandHandler(ISeveranceBenefitRepository SeveranceBenefitRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateSeveranceBenefitsCommand, ErrorOr<bool>>
{
    private readonly ISeveranceBenefitRepository _SeveranceBenefitRepository = SeveranceBenefitRepository ?? throw new ArgumentNullException(nameof(SeveranceBenefitRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateSeveranceBenefitsCommand command, CancellationToken cancellationToken)
    {
        string creationDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string editionDateString = creationDateString;

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("SeveranceBenefits.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("SeveranceBenefits.EditionDate", "EditionDate is not valid");


        List<SeveranceBenefit> severanceBenefitsToAdd = [];

        foreach (SeveranceBenefitData severanceBenefitData in command.SeveranceBenefitsDataList)
        {
            SeveranceBenefit severanceBenefit = new(
                new SeveranceBenefitId(Guid.NewGuid()),
                new CompanyId(Guid.Parse(severanceBenefitData.CompanyId)),
                severanceBenefitData.Name,
                severanceBenefitData.NameEnglish,
                severanceBenefitData.IsEditable,
                severanceBenefitData.IsDeleteable,
                creationDate,
                editionDate
            );
            severanceBenefitsToAdd.Add(severanceBenefit);
        }

        try
        {
            _SeveranceBenefitRepository.AddRangeSeveranceBenefits(severanceBenefitsToAdd);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}