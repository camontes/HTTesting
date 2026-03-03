using ErrorOr;
using HR_Platform.Application.CoexistenceCommitteeMembers.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using MediatR;

namespace HR_Platform.Application.CoexistenceCommitteeMembers.GetAllCollaborator;

internal sealed class GetAllCollaboratorCoexistenceCommitteeQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    IStringService stringService

    ) : IRequestHandler<GetAllCollaboratorCoexistenceCommitteeQuery, ErrorOr<List<CollaboratorCoexistenceCommitteeListResponse>>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));

    public async Task<ErrorOr<List<CollaboratorCoexistenceCommitteeListResponse>>> Handle(GetAllCollaboratorCoexistenceCommitteeQuery query, CancellationToken cancellationToken)
    {
        List<Collaborator> collaborators = await _collaboratorRepository.GetAll();
        List<CollaboratorCoexistenceCommitteeListResponse> collaboratorList = [];

        List<Collaborator> collaboratorNoCoexistenceCommittee = collaborators
            .Where(x => !x.IsCoexistenceCommitteeMember)
            .ToList();


        foreach (Collaborator collaborator in collaboratorNoCoexistenceCommittee)
        {
            CollaboratorCoexistenceCommitteeListResponse temp = new 
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