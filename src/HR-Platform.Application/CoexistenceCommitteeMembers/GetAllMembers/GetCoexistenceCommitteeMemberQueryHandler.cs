using ErrorOr;
using HR_Platform.Application.CoexistenceCommitteeMembers.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using MediatR;

namespace HR_Platform.Application.CoexistenceCommitteeMembers.GetAllMembers;

internal sealed class GetCoexistenceCommitteeMemberQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    IStringService stringService
    ) : IRequestHandler<GetCoexistenceCommitteeMemberQuery, ErrorOr<List<CoexistenceCommitteeMemberResponse>>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));

    public async Task<ErrorOr<List<CoexistenceCommitteeMemberResponse>>> Handle(GetCoexistenceCommitteeMemberQuery query, CancellationToken cancellationToken)
    {
        List<Collaborator> collaborators = await _collaboratorRepository.GetAll();
        List<Collaborator> collaboratorsResult = collaborators.Where(x => x.IsCoexistenceCommitteeMember).ToList();
        List<CoexistenceCommitteeMemberResponse> response = [];

        foreach (Collaborator item in collaboratorsResult)
        {
            CoexistenceCommitteeMemberResponse temp = new
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