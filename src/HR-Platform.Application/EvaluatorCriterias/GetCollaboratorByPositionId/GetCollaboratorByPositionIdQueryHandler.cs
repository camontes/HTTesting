using ErrorOr;
using HR_Platform.Application.BrigadeMembers.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Positions;
using MediatR;

namespace HR_Platform.Application.EvaluatorCriterias.GetCollaboratorByPositionId;

internal sealed class GetCollaboratorByPositionIdQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    IStringService stringService

    ) : IRequestHandler<GetCollaboratorByPositionIdQuery, ErrorOr<List<CollaboratorListResponse>>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));

    public async Task<ErrorOr<List<CollaboratorListResponse>>> Handle(GetCollaboratorByPositionIdQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByPositionId(new PositionId(query.PositonId)) is not List<Collaborator> collaboratorsByPosition)
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");

        List<CollaboratorListResponse> collaboratorListResponse = [];

        foreach (Collaborator collaborator in collaboratorsByPosition)
        {
            collaboratorListResponse.Add
            (
                new CollaboratorListResponse
                (
                    collaborator.Id.Value,
                    collaborator.Name,
                    collaborator.PersonalEmail.Value,
                    collaborator.BusinessEmail.Value,
                    _stringService.GetInitials(collaborator.Name),
                    collaborator.Photo
                )
            );
        }

        return collaboratorListResponse;
    }
}