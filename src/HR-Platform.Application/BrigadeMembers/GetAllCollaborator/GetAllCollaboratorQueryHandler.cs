using ErrorOr;
using HR_Platform.Application.BrigadeMembers.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.GetAllCollaborator;

internal sealed class GetAllCollaboratorQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    IStringService stringService

    ) : IRequestHandler<GetAllCollaboratorQuery, ErrorOr<List<CollaboratorListResponse>>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));

    public async Task<ErrorOr<List<CollaboratorListResponse>>> Handle(GetAllCollaboratorQuery query, CancellationToken cancellationToken)
    {
        List<Collaborator> collaborators = await _collaboratorRepository.GetAll();
        List<CollaboratorListResponse> collaboratorList = [];

        List<Collaborator> collaboratorNoLeaders = collaborators
            .Where(x => x.BusinessEmail.Value != "superadminth@exsis.com.co" )
            .ToList();

        foreach (Collaborator collaborator in collaboratorNoLeaders)
        {
            CollaboratorListResponse temp = new 
            (
                collaborator.Id.Value,
                collaborator.Name,
                collaborator.PersonalEmail.Value,
                collaborator.BusinessEmail.Value,
                _stringService.GetInitials(collaborator.Name),
                collaborator.Photo
            );
            collaboratorList.Add( temp );   
        }

        return collaboratorList;
    }
}