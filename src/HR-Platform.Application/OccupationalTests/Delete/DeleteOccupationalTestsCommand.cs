using ErrorOr;
using MediatR;

namespace HR_Platform.Application.OccupationalTests.Delete;

public record DeleteOccupationalTestsCommand
(
    Guid OccupationalTestId
) : IRequest<ErrorOr<bool>>;

