using ErrorOr;
using HR_Platform.Application.BrigadeMembers.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.BrigadeMembers;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.GetGellMember;

internal sealed class GetGellMemberQueryHandler(
    IBrigadeMemberRepository brigadeMemberRepository,
    IStringService stringService

    ) : IRequestHandler<GetAllMemberQuery, ErrorOr<BrigadeCommunication>>
{
    private readonly IBrigadeMemberRepository _brigadeMemberRepository = brigadeMemberRepository ?? throw new ArgumentNullException(nameof(brigadeMemberRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));

    public async Task<ErrorOr<BrigadeCommunication>> Handle(GetAllMemberQuery query, CancellationToken cancellationToken)
    {
        List<BrigadeMember> brigadeMembers = await _brigadeMemberRepository.GetAll();
        List<BrigadeMemberListResponse> memberList = [];

        List<BrigadeMembersResponse> nothing = [];
        List<BrigadeMembersResponse> mainLeaders = [];
        List<BrigadeMembersResponse> brigadeLeaders = [];
        List<BrigadeMembersResponse> members = [];


        foreach (BrigadeMember item in brigadeMembers)
        {
            BrigadeMembersResponse temp = new
            (
                item.Id.Value,
                item.CollaboratorId.Value,
                item.Collaborator.Name,
                item.Collaborator.BusinessEmail.Value,
                item.Collaborator.Position.Name, // Position
                item.BrigadeAdjustment.Name,
                item.BrigadeAdjustment.NameEnglish,
                item.BrigadeAdjustment.IconId,
                item.Collaborator.Photo,
                _stringService.GetInitials(item.Collaborator.Name),
                item.IsMainLeader,
                item.IsBrigadeLeader
            );
            if (item.IsMainLeader)
            {
                mainLeaders.Add(temp);
            }
            else if (item.IsBrigadeLeader)
            {
                brigadeLeaders.Add(temp);
            }
            else if (!item.IsMainLeader & !item.IsBrigadeLeader)
            {
                members.Add(temp);
            }
        }

        BrigadeCommunication result = new
        (
            brigadeMembers is not null && brigadeMembers.Count > 0 && brigadeMembers[0].IsVisible,
            brigadeMembers is not null && brigadeMembers.Count > 0 && brigadeMembers[0].IsVisible ? mainLeaders : nothing,
            brigadeMembers is not null && brigadeMembers.Count > 0 && brigadeMembers[0].IsVisible ? brigadeLeaders : nothing,
            brigadeMembers is not null && brigadeMembers.Count > 0 && brigadeMembers[0].IsVisible ? members : nothing
        );

        return result;
    }
}