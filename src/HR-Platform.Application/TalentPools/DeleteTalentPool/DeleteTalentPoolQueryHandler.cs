using ErrorOr;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.TalentPools;
using MediatR;

namespace HR_Platform.Application.TalentPools.DeleteTalentPoolQuery;

internal sealed class DeleteTalentPoolQueryHandler(
    ITalentPoolRepository talentPoolRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteTalentPoolQuery, ErrorOr<bool>>
{
    private readonly ITalentPoolRepository _talentPoolRepository = talentPoolRepository ?? throw new ArgumentNullException(nameof(talentPoolRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteTalentPoolQuery query, CancellationToken cancellationToken)
    {
        if (await _talentPoolRepository.GetByIdAsync(new TalentPoolId(query.Id)) is not TalentPool oldTalentPool)
            return Error.NotFound("TalentPool.NotFound", "The Talent Pool with the provide Id was not found.");
        
        try
        {
            _talentPoolRepository.Delete(oldTalentPool);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}