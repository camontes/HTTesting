using ErrorOr;
using MediatR;
using HR_Platform.Application.DreamMapAnswers.Common;

namespace HR_Platform.Application.DreamMapAnswers.GetByCollaboratorId;

public record GetDreamMapAnswersByCollaboratorIdQuery(string CollaboratorEmailOrId, bool IsByEmail) : IRequest<ErrorOr<DreamMapAnswersResponse>>;