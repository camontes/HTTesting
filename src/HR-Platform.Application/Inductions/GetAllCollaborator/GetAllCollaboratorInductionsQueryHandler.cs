using ErrorOr;
using HR_Platform.Application.BrigadeMembers.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorInductions;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Inductions;
using MediatR;

namespace HR_Platform.Application.Inductions.GetAllCollaborator;

internal sealed class GetAllCollaboratorInductionsQueryHandler(
    ICollaboratorInductionRepository collaboratorInductionRepository,
    ICollaboratorRepository collaboratorRepository,
    IStringService stringService

    ) : IRequestHandler<GetAllCollaboratorInductionsQuery, ErrorOr<List<CollaboratorListResponse>>>
{
    private readonly ICollaboratorInductionRepository _collaboratorInductionRepository = collaboratorInductionRepository ?? throw new ArgumentNullException(nameof(collaboratorInductionRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));

    public async Task<ErrorOr<List<CollaboratorListResponse>>> Handle(GetAllCollaboratorInductionsQuery query, CancellationToken cancellationToken)
    {
        List<Collaborator> collaborators = await _collaboratorRepository.GetAll();
        List<CollaboratorListResponse> collaboratorList = [];

        List<CollaboratorInduction> collaboratorInductions = await _collaboratorInductionRepository.GetByInductionIdAsync(new InductionId(query.InductionId));

        IEnumerable<CollaboratorId> collaboratorIds = collaboratorInductions.Select(z => z.CollaboratorId).ToList();

        List<Collaborator> collaboratorNoInductionFinished = collaborators
            .Where(x => !collaboratorIds
            .Contains(x.Id) )
            .ToList();

        foreach (Collaborator collaborator in collaboratorNoInductionFinished)
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
            collaboratorList.Add(temp);
        }
        return collaboratorList;
    }
}