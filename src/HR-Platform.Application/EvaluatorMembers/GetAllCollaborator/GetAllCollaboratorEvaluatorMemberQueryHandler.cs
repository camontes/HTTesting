using ErrorOr;
using HR_Platform.Application.EvaluatorMembers.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using MediatR;

namespace HR_Platform.Application.EvaluatorMembers.GetAllCollaborator;

internal sealed class GetAllCollaboratorEvaluatorMemberQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    IStringService stringService

    ) : IRequestHandler<GetAllCollaboratorEvaluatorMemberQuery, ErrorOr<List<CollaboratorEvaluatorMemberListResponse>>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));

    public async Task<ErrorOr<List<CollaboratorEvaluatorMemberListResponse>>> Handle(GetAllCollaboratorEvaluatorMemberQuery query, CancellationToken cancellationToken)
    {
        List<Collaborator> collaborators = await _collaboratorRepository.GetAll();
        List<CollaboratorEvaluatorMemberListResponse> collaboratorList = [];

        List<Collaborator> collaboratorNoCoexistenceCommittee = collaborators
            .Where(x => !x.IsEvaluator)
            .ToList();


        foreach (Collaborator collaborator in collaboratorNoCoexistenceCommittee)
        {
            CollaboratorEvaluatorMemberListResponse temp = new 
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