using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.TalentPools;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.TalentPools.Create;

internal sealed class CreateTalentPoolsCommandHandler(ITalentPoolRepository talentPoolRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateTalentPoolsCommand, ErrorOr<bool>>
{
    private readonly ITalentPoolRepository _talentPoolRepository = talentPoolRepository ?? throw new ArgumentNullException(nameof(talentPoolRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateTalentPoolsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("TalentPools.CreationDate", "CreationDate is not valid");

        TalentPool talentPool = new(
            new TalentPoolId(Guid.NewGuid()),
            new CompanyId(command.CompanyId),
            command.Tittle,
            command.Description,
            false, //IsArchived
             true, //IsEditable
             true, //IsDeleteable
            creationDate,
            creationDate
        );
        try
        {
            _talentPoolRepository.Add(talentPool);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}