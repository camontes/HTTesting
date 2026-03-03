using ErrorOr;
using HR_Platform.Application.OrganizationCharts.Common;
using MediatR;

namespace HR_Platform.Application.OrganizationCharts.GetById;

public record GetOrganizationChartByIdQuery(Guid OrganizationChartId) : IRequest<ErrorOr<OrganizationChartFileResponse>>;