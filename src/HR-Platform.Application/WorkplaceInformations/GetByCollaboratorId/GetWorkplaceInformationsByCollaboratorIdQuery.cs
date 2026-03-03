using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using MediatR;

namespace HR_Platform.Application.WorkplaceInformations.GetByCollaboratorId;

public record GetWorkplaceInformationsByCollaboratorIdQuery(string? CollaboratorId, string EmailChangeBy, string Year) : IRequest<ErrorOr<WorkplaceInformationsResponse>>;