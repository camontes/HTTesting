using ErrorOr;
using HR_Platform.Application.TimeZones.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllTimeZonesQuery() : IRequest<ErrorOr<IReadOnlyList<TimeZonesResponse>>>;