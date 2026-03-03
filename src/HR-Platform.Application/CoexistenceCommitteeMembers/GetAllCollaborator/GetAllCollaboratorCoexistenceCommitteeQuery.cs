using ErrorOr;
using HR_Platform.Application.CoexistenceCommitteeMembers.Common;
using MediatR;

namespace HR_Platform.Application.CoexistenceCommitteeMembers.GetAllCollaborator;

public record GetAllCollaboratorCoexistenceCommitteeQuery(Guid CompanyId) : IRequest<ErrorOr<List<CollaboratorCoexistenceCommitteeListResponse>>>;