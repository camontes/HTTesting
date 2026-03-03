using ErrorOr;
using MediatR;

namespace HR_Platform.Application.WorkplaceInformations.Delete;

public record DeleteWorkplaceInformationsCommand
(
    Guid WorkplaceInformationId
) : IRequest<ErrorOr<bool>>;

