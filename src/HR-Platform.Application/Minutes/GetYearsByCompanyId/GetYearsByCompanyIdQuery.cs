using ErrorOr;
using HR_Platform.Application.Minutes.Common;
using MediatR;

namespace HR_Platform.Application.Minutes.GetYearsByCompanyId;

public record GetYearsByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<MinuteYearsListResponse>>;
