using ErrorOr;
using HR_Platform.Domain.BrigadeMembers;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.Delete;

internal sealed class DeleteBrigadeMemberCommandHandler(
    IBrigadeMemberRepository brigadeMemberRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteBrigadeMembersCommand, ErrorOr<bool>>
{
    private readonly IBrigadeMemberRepository _brigadeMemberRepository = brigadeMemberRepository ?? throw new ArgumentNullException(nameof(brigadeMemberRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteBrigadeMembersCommand command, CancellationToken cancellationToken)
    {
        List<BrigadeMember> membersList = await _brigadeMemberRepository.GetAll();

        try
        {
            if (membersList.Count != 0)
            {
                _brigadeMemberRepository.DeleteRange(membersList);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}