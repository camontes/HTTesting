using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Forms.Create;

public record CreateBaseFormCommand
(
    string Name,
    string Description,
    Guid NoveltyTypeId,
    List<QuestionTypeRequest> QuestionTypeRequests
) : IRequest<ErrorOr<bool>>;

public record QuestionTypeRequest
(
    Guid QuestionTypeId,
    string Question,
    bool IsRequired
);
