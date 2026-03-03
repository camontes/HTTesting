using ErrorOr;
using MediatR;

namespace HR_Platform.Application.DreamMapAnswers.Create;

public record CreateDreamMapAnswersCommand(string CollaboratorEmail, List<DreamMapAnswerData> DreamMapAnswersDataList, int TemplateIndicator) : IRequest<ErrorOr<bool>>;

public record DreamMapAnswerData(
    string Question,
    string Answer
);

