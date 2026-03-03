using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Domain.Collaborators;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetById;

public record GetCollaboratorByIdQuery(CollaboratorId Id) : IRequest<ErrorOr<CollaboratorsByIdResponse>>;