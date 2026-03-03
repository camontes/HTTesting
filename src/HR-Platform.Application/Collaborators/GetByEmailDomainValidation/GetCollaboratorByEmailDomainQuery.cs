using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByEmail;

public record GetCollaboratorByEmailDomainQuery(string Email) : IRequest<ErrorOr<bool>>;