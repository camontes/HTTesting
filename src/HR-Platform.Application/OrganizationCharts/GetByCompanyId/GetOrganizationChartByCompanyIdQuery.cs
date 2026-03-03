using ErrorOr;
using HR_Platform.Application.OrganizationCharts.Common;
using MediatR;

namespace HR_Platform.Application.OrganizationCharts.GetByCompanyId;

public record GetOrganizationChartByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<OrganizationChartFileResponse?>>;