using ErrorOr;
using HR_Platform.Application.WorkplaceEvidences.Common;
using MediatR;

namespace HR_Platform.Application.WorkplaceEvidences.GetYearsByCollaboratorId;

public record GetWorkplaceEvidenceYearsByCollaboratorIdQuery(Guid CollaboratorId) : IRequest<ErrorOr<WorkplaceEvidenceFileYearsListResponse>>;


