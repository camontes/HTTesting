using ErrorOr;
using HR_Platform.Domain.BrigadeMembers;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.DeletedBrigadeNotification;

internal sealed class DeletedBrigadeNotificationQueryHandler(
    IBrigadeMemberRepository brigadeMemberRepository

    ) : IRequestHandler<DeletedBrigadeNotificationQuery, ErrorOr<bool>>
{
    private readonly IBrigadeMemberRepository _brigadeMemberRepository = brigadeMemberRepository ?? throw new ArgumentNullException(nameof(brigadeMemberRepository));

    public async Task<ErrorOr<bool>> Handle(DeletedBrigadeNotificationQuery query, CancellationToken cancellationToken)
    {
        List<BrigadeMember> brigadeMembers = await _brigadeMemberRepository.GetAll();

        return brigadeMembers.Any(x => x.HasBeenDeletedBrigadeAdjustment);
    }
}