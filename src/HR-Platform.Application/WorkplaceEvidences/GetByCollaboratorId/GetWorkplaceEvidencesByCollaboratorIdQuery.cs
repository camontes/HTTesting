using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using MediatR;

namespace HR_Platform.Application.WorkplaceEvidences.GetByCollaboratorId;

public record GetWorkplaceEvidencesByCollaboratorIdQuery(string? CollaboratorId, string Year, string EmailChangeBy) : IRequest<ErrorOr<WorkplaceEvidencesResponse>>;