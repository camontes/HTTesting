using ErrorOr;
using HR_Platform.Application.DreamMapQuestions.Create;
using MediatR;

namespace HR_Platform.Application.DreamMapQuestionEntities.Create;

public record CreateBaseDreamMapQuestionsCommand(List<DreamMapQuestionData> DreamMapQuestionList) : IRequest<ErrorOr<bool>>;



