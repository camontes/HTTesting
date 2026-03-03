using ErrorOr;
using HR_Platform.Domain.OrganizationCharts;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.OrganizationCharts.Delete;

internal sealed class DeleteOrganizationChartCommandHandler(
    IOrganizationChartRepository organizationChartRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteOrganizationChartsCommand, ErrorOr<bool>>
{
    private readonly IOrganizationChartRepository _organizationChartRepository = organizationChartRepository ?? throw new ArgumentNullException(nameof(organizationChartRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteOrganizationChartsCommand query, CancellationToken cancellationToken)
    {
        if (await _organizationChartRepository.GetByIdAsync(new OrganizationChartId(query.OrganizationChartId)) is not OrganizationChart organizationChart)
            return Error.NotFound("OrganizationChart.NotFound", "The OrganizationChart with the provide Id was not found.");

        try
        {
            _organizationChartRepository.Delete(organizationChart);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}