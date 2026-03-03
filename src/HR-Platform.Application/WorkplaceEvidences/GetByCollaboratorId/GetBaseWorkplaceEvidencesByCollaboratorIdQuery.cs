using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using MediatR;

namespace HR_Platform.Application.WorkplaceEvidences.GetByCollaboratorId;

public record GetBaseWorkplaceEvidencesByCollaboratorIdQuery(string? CollaboratorId, string Year) : IRequest<ErrorOr<WorkplaceEvidencesResponse>>;