using ErrorOr;
using HR_Platform.Application.OccupationalRecommendations.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.OccupationalRecommendations;
using MediatR;

namespace HR_Platform.Application.OccupationalRecommendations.GetYearsByCollaboratorId;

internal sealed class GetYearsByCollaboratorIdQueryHandler(
    IOccupationalRecommendationRepository occupationalRecommendationRepository,
    ICollaboratorRepository collaboratorRepository

    ) : IRequestHandler<GetYearsByCollaboratorIdQuery, ErrorOr<OccupationalRecommendationFileYearsListResponse>>
{
    private readonly IOccupationalRecommendationRepository _occupationalRecommendationRepository =
        occupationalRecommendationRepository ?? throw new ArgumentNullException(nameof(occupationalRecommendationRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));

    public async Task<ErrorOr<OccupationalRecommendationFileYearsListResponse>> Handle(GetYearsByCollaboratorIdQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(query.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The Collaborator with the provide Id was not found.");

        List<OccupationalRecommendation>? occupationalRecommendationsList =
            await _occupationalRecommendationRepository.GetByCollaboratorIdAsync(oldCollaborator.Id, string.Empty);

        List<string> distinctYears = [];

        if (occupationalRecommendationsList is not null && occupationalRecommendationsList.Count > 0)
        {
            distinctYears =
                occupationalRecommendationsList
                .Select(m => m.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        OccupationalRecommendationFileYearsListResponse response = new
        (
            distinctYears
        );

        return response;

    }
}

