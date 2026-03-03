using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Domain.Collaborators;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetContractById;
public record GetContractByIdQuery(CollaboratorId Id) : IRequest<ErrorOr<CollaboratorContractResponse>>;
