using ErrorOr;
using HR_Platform.Domain.BrigadeMembers;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.UpdateIsVisible;

internal sealed class UpdateIsVisibleBrigadeMembersCommandHandler(
    IBrigadeMemberRepository brigadeMemberRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateIsVisibleBrigadeMembersCommand, ErrorOr<bool>>
{
    private readonly IBrigadeMemberRepository _brigadeMemberRepository = brigadeMemberRepository ?? throw new ArgumentNullException(nameof(brigadeMemberRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateIsVisibleBrigadeMembersCommand query, CancellationToken cancellationToken)
    {
        List<BrigadeMember> brigadeMemberList = await _brigadeMemberRepository.GetAll();

        if (brigadeMemberList.Count > 0)
        {
            foreach (BrigadeMember member in brigadeMemberList)
            {
                member.IsVisible = !member.IsVisible;
            }

        _brigadeMemberRepository.UpdateRange(brigadeMemberList);
        }

        //Missing Save
        // Add the Field in Get By Id 
        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}