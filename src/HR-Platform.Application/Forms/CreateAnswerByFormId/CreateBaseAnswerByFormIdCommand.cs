using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Forms.CreateAnswerByFormId;

public record CreateBaseAnswerByFormIdCommand(
    List<AnswerObject> AnswerObjects,
    Guid NoveltyTypeId
) : IRequest<ErrorOr<bool>>;

public record AnswerObject
(
    Guid FormQuestionsTypeId,
    string Answer
);