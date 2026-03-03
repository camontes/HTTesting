using ErrorOr;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.TalentPools;
using MediatR;

namespace HR_Platform.Application.TalentPools.UpdateArchivedStateById;

internal sealed class UpdateArchivedStateByIdQueryHandler(
    ITalentPoolRepository talentPoolRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateArchivedStateByIdQuery, ErrorOr<bool>>
{
    private readonly ITalentPoolRepository _talentPoolRepository = talentPoolRepository ?? throw new ArgumentNullException(nameof(talentPoolRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateArchivedStateByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _talentPoolRepository.GetByIdAsync(new TalentPoolId(query.TalentPoolId)) is not TalentPool oldTalentPool)
            return Error.NotFound("TalentPool.NotFound", "The Talent Pool with the provide Id was not found.");

        oldTalentPool.IsArchived = !oldTalentPool.IsArchived;

        try
        {
            _talentPoolRepository.Update(oldTalentPool);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}