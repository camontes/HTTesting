using ErrorOr;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.TalentPools;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.TalentPools.Update;

internal sealed class UpdateTalentPoolsCommandHandler(ITalentPoolRepository talentPoolRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateTalentPoolsCommand, ErrorOr<bool>>
{
    private readonly ITalentPoolRepository _talentPoolRepository = talentPoolRepository ?? throw new ArgumentNullException(nameof(talentPoolRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateTalentPoolsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editationDateString) is not TimeDate editationDate)
            return Error.Validation("TalentPools.EditationDate", "EditationDate is not valid");

        if (await _talentPoolRepository.GetByIdAsync(new TalentPoolId(Guid.TryParse(command.TalentPoolId, out Guid talentPoolIdGuid) ? talentPoolIdGuid : Guid.NewGuid())) is not TalentPool oldTalentPool)
            return Error.Validation("TalentPools.getById", "Talent Pool With Provide Id Was Not Found");

        int contChanges = 0;

        if (command.Tittle != oldTalentPool.Tittle)
        {
            oldTalentPool.Tittle = command.Tittle;
            contChanges++;
        }
        if (command.Description != oldTalentPool.Description)
        {
            oldTalentPool.Description = command.Description;
            contChanges++;
        }

        try
        {
            if (contChanges > 0)
            {
                oldTalentPool.EditionDate = editationDate;
                _talentPoolRepository.Update(oldTalentPool);
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