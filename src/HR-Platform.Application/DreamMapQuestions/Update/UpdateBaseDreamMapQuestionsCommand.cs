using ErrorOr;
using HR_Platform.Application.DreamMapQuestions.Update;
using MediatR;

namespace HR_Platform.Application.DreamMapQuestionEntities.Update;

public record UpdateBaseDreamMapQuestionsCommand(List<DreamMapQuestionUpdate> DreamMapQuestionList) : IRequest<ErrorOr<bool>>;



