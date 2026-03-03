using ErrorOr;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.TalentPools;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.TalentPools.DuplicateTalentPool;

internal sealed class DuplicateTalentPoolQueryHandler(
    ITalentPoolRepository talentPoolRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DuplicateTalentPoolQuery, ErrorOr<bool>>
{
    private readonly ITalentPoolRepository _talentPoolRepository = talentPoolRepository ?? throw new ArgumentNullException(nameof(talentPoolRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DuplicateTalentPoolQuery query, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("TalentPools.CreationDate", "CreationDate is not valid");

        if (await _talentPoolRepository.GetByIdAsync(new TalentPoolId(query.Id)) is not TalentPool oldTalentPool)
            return Error.NotFound("TalentPool.NotFound", "The Talent Pool with the provide Id was not found.");

        TalentPool talentPool = new(
             new TalentPoolId(Guid.NewGuid()),
             oldTalentPool.CompanyId,
             $"{oldTalentPool.Tittle}-copia",
             oldTalentPool.Description,
             oldTalentPool.IsArchived, //IsArchived
             oldTalentPool.IsEditable, //IsEditable
             oldTalentPool.IsDeleteable, //IsDeleteable
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