using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EvaluatorMembers.Create;

public record CreateEvaluatorMemberCommand(List<Guid> CollaboratorIds) : IRequest<ErrorOr<bool>>;



