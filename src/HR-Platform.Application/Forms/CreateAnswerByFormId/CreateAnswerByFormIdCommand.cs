using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Forms.CreateAnswerByFormId;

public record CreateAnswerByFormIdCommand(
    string EmailWhoIsLogin,
    List<AnswerObject> AnswerObjects,
    Guid NoveltyTypeId
) : IRequest<ErrorOr<bool>>;
