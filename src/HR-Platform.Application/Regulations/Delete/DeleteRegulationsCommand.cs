using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Regulations.Delete;

public record DeleteRegulationsCommand
(
    Guid RegulationId
) : IRequest<ErrorOr<bool>>;

