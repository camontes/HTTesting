using ErrorOr;
using MediatR;
using HR_Platform.Application.CopasstMembers.Common;

namespace HR_Platform.Application.CopasstMembers.GetAllCollaborator;

public record GetAllCollaboratorCopasstQuery(Guid CompanyId) : IRequest<ErrorOr<List<CollaboratorCopasstListResponse>>>;