using ErrorOr;
using MediatR;

namespace HR_Platform.Application.DreamMapQuestions.Update;

public record UpdateDreamMapQuestionsCommand(Guid CompanyId, List<DreamMapQuestionUpdate> DreamMapQuestionsDataList) : IRequest<ErrorOr<bool>>;

public record DreamMapQuestionUpdate(
    Guid QuestionId,
    string Question
);

