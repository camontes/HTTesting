using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BrigadeDocumentations.Delete;

public record DeleteBrigadeDocumentationsCommand
(
    Guid BrigadeDocumentationId
) : IRequest<ErrorOr<bool>>;

