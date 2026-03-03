using ErrorOr;
using MediatR;
using HR_Platform.Application.DreamMapAnswers.Common;

namespace HR_Platform.Application.DreamMapAnswers.GetByCompanyId;

public record GetDreamMapAnswersByCompanyIdQuery() : IRequest<ErrorOr<List<DreamMapAnswersCollaboratorResponse>>>;