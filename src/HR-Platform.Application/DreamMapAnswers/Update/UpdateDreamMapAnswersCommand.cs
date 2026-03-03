using ErrorOr;
using MediatR;

namespace HR_Platform.Application.DreamMapAnswers.Update;

public record UpdateDreamMapAnswersCommand(string CollaboratorEmail, List<DreamMapAnswerUpdate> DreamMapAnswersDataList, int TemplateIndicator) : IRequest<ErrorOr<bool>>;

public record DreamMapAnswerUpdate(
    Guid AnsewerId,
    string Answer
);

