using ErrorOr;
using HR_Platform.Application.EvaluatorMembers.Common;
using MediatR;

namespace HR_Platform.Application.EvaluatorMembers.GetAllMembers;

public record GetEvaluatorMemberQuery() : IRequest<ErrorOr<List<EvaluatorMemberResponse>>>;