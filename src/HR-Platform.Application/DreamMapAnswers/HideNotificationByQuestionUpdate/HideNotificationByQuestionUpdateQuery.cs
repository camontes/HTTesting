using ErrorOr;
using MediatR;
using HR_Platform.Application.DreamMapAnswers.Common;

namespace HR_Platform.Application.DreamMapAnswers.GetByCollaboratorId;

public record HideNotificationByQuestionUpdateQuery(string CollaboratorEmail) : IRequest<ErrorOr<bool>>;