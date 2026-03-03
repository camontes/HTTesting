using ErrorOr;
using HR_Platform.Application.CoexistenceCommitteeMinutes.Common;
using MediatR;

namespace HR_Platform.Application.CoexistenceCommitteeMinutes.GetYearsByCompanyId;

public record GetYearsByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<CoexistenceCommitteeMinuteYearsListResponse>>;

