using ErrorOr;
using HR_Platform.Application.CopasstMembers.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using MediatR;

namespace HR_Platform.Application.CopasstMembers.GetAllMembers;

internal sealed class GetCopasstMemberQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    IStringService stringService
    ) : IRequestHandler<GetCopasstMemberQuery, ErrorOr<List<CopasstMemberResponse>>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));

    public async Task<ErrorOr<List<CopasstMemberResponse>>> Handle(GetCopasstMemberQuery query, CancellationToken cancellationToken)
    {
        List<Collaborator> collaborators = await _collaboratorRepository.GetAll();
        List<Collaborator> collaboratorsResult = collaborators.Where(x => x.IsCopasstMember).ToList();
        List<CopasstMemberResponse> response = [];

        foreach (Collaborator item in collaboratorsResult)
        {
            CopasstMemberResponse temp = new
            (
                item.Id.Value,
                item.Name,
                item.Position.Name,
                item.Photo,
                _stringService.GetInitials(item.Name)
            );
            response.Add( temp );
        }

        return response;
    }
}