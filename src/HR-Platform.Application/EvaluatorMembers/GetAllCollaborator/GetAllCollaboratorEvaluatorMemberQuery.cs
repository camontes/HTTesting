using ErrorOr;
using HR_Platform.Application.EvaluatorMembers.Common;
using MediatR;

namespace HR_Platform.Application.EvaluatorMembers.GetAllCollaborator;

public record GetAllCollaboratorEvaluatorMemberQuery(Guid CompanyId) : IRequest<ErrorOr<List<CollaboratorEvaluatorMemberListResponse>>>;