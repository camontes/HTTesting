using ErrorOr;
using HR_Platform.Application.WorkplaceEvidences.Common;
using HR_Platform.Application.WorkplaceRecommendations.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.WorkplaceEvidences;
using HR_Platform.Domain.WorkplaceRecommendations;
using MediatR;

namespace HR_Platform.Application.WorkplaceRecommendations.GetYearsByCollaboratorId;

internal sealed class GetYearsByCollaboratorIdQueryHandler(
    IWorkplaceRecommendationRepository workplaceRecommendationRepository,
    ICollaboratorRepository collaboratorRepository

    ) : IRequestHandler<GetWorkplaceRecommendationYearsByCollaboratorIdQuery, ErrorOr<WorkplaceRecommendationFileYearsListResponse>>
{
    private readonly IWorkplaceRecommendationRepository _workplaceRecommendationRepository =
        workplaceRecommendationRepository ?? throw new ArgumentNullException(nameof(workplaceRecommendationRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));

    public async Task<ErrorOr<WorkplaceRecommendationFileYearsListResponse>> Handle(GetWorkplaceRecommendationYearsByCollaboratorIdQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(query.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The Collaborator with the provide Id was not found.");

        List<WorkplaceRecommendation>? recommendationList = await _workplaceRecommendationRepository.GetByCollaboratorIdAsync(oldCollaborator.Id, string.Empty);

        List<string> distinctYears = [];

        if (recommendationList is not null && recommendationList.Count > 0)
        {
            distinctYears =
                recommendationList
                .Select(r => r.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        WorkplaceRecommendationFileYearsListResponse response = new
        (
            distinctYears
        );

        return response;

    }
}

