using ErrorOr;
using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.BrigadeMembers;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.HasThereBeenUpdate;

internal sealed class HasThereBeenUpdateQueryHandler(
    IBrigadeMemberRepository brigadeMemberRepository,
    IBrigadeAdjustmentRepository brigadeAdjustmentRepository
    ) : IRequestHandler<HasThereBeenUpdateQuery, ErrorOr<bool>>
{
    private readonly IBrigadeAdjustmentRepository _brigadeAdjustmentRepository = brigadeAdjustmentRepository ?? throw new ArgumentNullException(nameof(brigadeAdjustmentRepository));
    private readonly IBrigadeMemberRepository _brigadeMemberRepository = brigadeMemberRepository ?? throw new ArgumentNullException(nameof(brigadeMemberRepository));

    public async Task<ErrorOr<bool>> Handle(HasThereBeenUpdateQuery query, CancellationToken cancellationToken)
    {
        List<BrigadeAdjustment> brigadeAdjustments = await _brigadeAdjustmentRepository.GetAll();
        List<BrigadeMember> brigadeMembers = await _brigadeMemberRepository.GetAll();

        if (brigadeAdjustments is not null && brigadeAdjustments.Count > 0)
        {
            BrigadeAdjustment lastBrigadeIncluded = brigadeAdjustments.OrderByDescending(x => x.EditionDate.Value).ToList()[0];
            return brigadeMembers.Any(x => x.EditionDate.Value < lastBrigadeIncluded.EditionDate.Value);
        }
        return false;
    }
}