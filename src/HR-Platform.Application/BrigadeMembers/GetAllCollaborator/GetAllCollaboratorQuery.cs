using ErrorOr;
using MediatR;
using HR_Platform.Application.BrigadeMembers.Common;

namespace HR_Platform.Application.BrigadeMembers.GetAllCollaborator;

public record GetAllCollaboratorQuery(Guid CompanyId) : IRequest<ErrorOr<List<CollaboratorListResponse>>>;