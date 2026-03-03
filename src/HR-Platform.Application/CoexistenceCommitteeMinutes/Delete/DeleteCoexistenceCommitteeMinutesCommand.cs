using ErrorOr;
using MediatR;

namespace HR_Platform.Application.CoexistenceCommitteeMinutes.Delete;

public record DeleteCoexistenceCommitteeMinutesCommand
(
    Guid CoexistenceCommitteeMinuteId
) : IRequest<ErrorOr<bool>>;

