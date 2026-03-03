using ErrorOr;
using MediatR;

namespace HR_Platform.Application.DreamMapAnswers.Update;

public record UpdateBaseDreamMapAnswersCommand(List<DreamMapAnswerUpdate> DreamMapAnswersDataList, int TemplateIndicator) : IRequest<ErrorOr<bool>>;
