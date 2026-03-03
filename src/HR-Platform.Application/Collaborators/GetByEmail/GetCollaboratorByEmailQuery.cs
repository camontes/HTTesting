using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByEmail;

public record GetCollaboratorByEmailQuery(string Email) : IRequest<ErrorOr<CollaboratorsResponse>>;