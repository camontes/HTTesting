using ErrorOr;
using HR_Platform.Application.CoexistenceCommitteeMinutes.Common;
using MediatR;

namespace HR_Platform.Application.CoexistenceCommitteeMinutes.GetById;

public record GetCoexistenceCommitteeMinuteByIdQuery(Guid CoexistenceCommitteeMinuteId) : IRequest<ErrorOr<CoexistenceCommitteeMinuteFileResponse>>;