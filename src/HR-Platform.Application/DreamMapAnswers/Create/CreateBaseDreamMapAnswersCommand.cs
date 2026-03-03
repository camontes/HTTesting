using ErrorOr;
using MediatR;

namespace HR_Platform.Application.DreamMapAnswers.Create;

public record CreateBaseDreamMapAnswersCommand(List<DreamMapAnswerData> DreamMapAnswersDataList, int TemplateIndicator) : IRequest<ErrorOr<bool>>;
