using ErrorOr;
using HR_Platform.Application.BrigadeMembers.Common;
using HR_Platform.Application.EvaluatorCriterias.Common;
using MediatR;

namespace HR_Platform.Application.EvaluatorCriterias.GetCollaboratorByEvaluator;

public record GetCollaboratorByEvaluatorQuery(string EmailWhoIsLogin) : IRequest<ErrorOr<List<CollaboratorByEvalutorResponse>>>;