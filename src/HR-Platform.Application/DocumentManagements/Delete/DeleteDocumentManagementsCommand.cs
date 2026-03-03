using ErrorOr;
using MediatR;

namespace HR_Platform.Application.DocumentManagements.Delete;

public record DeleteDocumentManagementsCommand
(
    Guid DocumentManagementId
) : IRequest<ErrorOr<bool>>;

