using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Forms.Create;

public record CreateFormCommand
(
    Guid CompanyId,
    string Name,
    string Description,
    Guid NoveltyTypeId,
    List<QuestionTypeRequest> QuestionTypeRequests
) : IRequest<ErrorOr<bool>>;

