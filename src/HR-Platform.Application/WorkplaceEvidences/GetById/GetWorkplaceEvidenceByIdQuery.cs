using ErrorOr;
using HR_Platform.Application.WorkplaceEvidences.Common;
using MediatR;

namespace HR_Platform.Application.WorkplaceEvidences.GetById;

public record GetWorkplaceEvidenceByIdQuery(Guid WorkplaceEvidenceId) : IRequest<ErrorOr<WorkplaceEvidenceFileResponse>>;