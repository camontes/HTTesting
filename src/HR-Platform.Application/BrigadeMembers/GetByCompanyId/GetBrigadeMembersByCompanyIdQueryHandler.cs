using ErrorOr;
using HR_Platform.Application.BrigadeMembers.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.BrigadeMembers;
using HR_Platform.Domain.Companies;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.GetByCompanyId;

internal sealed class GetBrigadeMembersByCompanyIdHandler(
    IBrigadeMemberRepository brigadeMemberRepository,
    ICompanyRepository companyRepository,
    IStringService stringService

    ) : IRequestHandler<GetBrigadeMembersByCompanyIdQuery, ErrorOr<BrigadeResponse>>
{
    private readonly IBrigadeMemberRepository _brigadeMemberRepository = brigadeMemberRepository ?? throw new ArgumentNullException(nameof(brigadeMemberRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));

    public async Task<ErrorOr<BrigadeResponse>> Handle(GetBrigadeMembersByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        List<BrigadeMember> brigadeMembers = await _brigadeMemberRepository.GetAll();
        List<BrigadeMembersResponse> mainLeaders = [];
        List<BrigadeMembersResponse> brigadeLeaders = [];
        List<BrigadeMembersResponse> members = [];

        #region StructureResult
        //{
        //    "LiderPrincipal": {
        //        "Nombre" :  "Example",
        //    },
        //    "Brigada_1" : {
        //        "Lider": {
        //            "Nombre" :  "Example"
        //        },
        //        "Integrantes": [
        //            {
        //            "Nombre" :  "Example"
        //            },
        //            {
        //            "Nombre" :  "Example"
        //            }
        //        ]
        //    }
        //}
        #endregion

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

        BrigadeResponse response = new
        (
            mainLeaders,
            brigadeLeaders,
            members,
            brigadeMembers is not null && brigadeMembers.Count > 0 && brigadeMembers[0].IsVisible
        );

        return response;

    }
}