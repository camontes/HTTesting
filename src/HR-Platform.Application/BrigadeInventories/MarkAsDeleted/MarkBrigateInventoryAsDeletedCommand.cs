using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BrigadeInventories.MarkAsDeleted;

public record MarkBrigateInventoryAsDeletedCommand
(
    Guid Id
)
:
IRequest<ErrorOr<bool>>;