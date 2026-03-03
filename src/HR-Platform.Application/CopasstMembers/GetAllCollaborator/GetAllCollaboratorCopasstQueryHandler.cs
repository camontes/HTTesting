using ErrorOr;
using HR_Platform.Application.CopasstMembers.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using MediatR;

namespace HR_Platform.Application.CopasstMembers.GetAllCollaborator;

internal sealed class GetAllCollaboratorCopasstQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    IStringService stringService

    ) : IRequestHandler<GetAllCollaboratorCopasstQuery, ErrorOr<List<CollaboratorCopasstListResponse>>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));

    public async Task<ErrorOr<List<CollaboratorCopasstListResponse>>> Handle(GetAllCollaboratorCopasstQuery query, CancellationToken cancellationToken)
    {
        List<Collaborator> collaborators = await _collaboratorRepository.GetAll();
        List<CollaboratorCopasstListResponse> collaboratorList = [];

        List<Collaborator> collaboratorNoCopasst = collaborators
            .Where(x => !x.IsCopasstMember)
            .ToList();


        foreach (Collaborator collaborator in collaboratorNoCopasst)
        {
            CollaboratorCopasstListResponse temp = new 
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