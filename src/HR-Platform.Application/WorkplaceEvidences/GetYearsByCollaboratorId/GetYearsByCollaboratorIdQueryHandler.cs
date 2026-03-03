using ErrorOr;
using HR_Platform.Application.WorkplaceEvidences.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.WorkplaceEvidences;
using MediatR;

namespace HR_Platform.Application.WorkplaceEvidences.GetYearsByCollaboratorId;

internal sealed class GetYearsByCollaboratorIdQueryHandler(
    IWorkplaceEvidenceRepository workplaceEvidenceRepository,
    ICollaboratorRepository collaboratorRepository

    ) : IRequestHandler<GetWorkplaceEvidenceYearsByCollaboratorIdQuery, ErrorOr<WorkplaceEvidenceFileYearsListResponse>>
{
    private readonly IWorkplaceEvidenceRepository _workplaceEvidenceRepository =
        workplaceEvidenceRepository ?? throw new ArgumentNullException(nameof(workplaceEvidenceRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));

    public async Task<ErrorOr<WorkplaceEvidenceFileYearsListResponse>> Handle(GetWorkplaceEvidenceYearsByCollaboratorIdQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(query.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The Collaborator with the provide Id was not found.");

        List<WorkplaceEvidence>? regulationList = await _workplaceEvidenceRepository.GetByCollaboratorIdAsync(oldCollaborator.Id, string.Empty);

        List<string> distinctYears = [];

        if (regulationList is not null && regulationList.Count > 0)
        {
            distinctYears =
                regulationList
                .Select(r => r.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        WorkplaceEvidenceFileYearsListResponse response = new
        (
            distinctYears
        );

        return response;

    }
}

