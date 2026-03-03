using ErrorOr;
using MediatR;

namespace HR_Platform.Application.DreamMapQuestions.Create;

public record CreateDreamMapQuestionsCommand(Guid CompanyId, List<DreamMapQuestionData> DreamMapQuestionsDataList) : IRequest<ErrorOr<bool>>;

public record DreamMapQuestionData(
    string Question
);

