using ErrorOr;
using MediatR;

namespace HR_Platform.Application.OrganizationCharts.Delete;

public record DeleteOrganizationChartsCommand
(
    Guid OrganizationChartId
) : IRequest<ErrorOr<bool>>;

