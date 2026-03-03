using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.Areas.Common;
using HR_Platform.Domain.Areas;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllAreasQueryHandler(
    IAreaRepository AreaRepository
    ) : IRequestHandler<GetAllAreasQuery, ErrorOr<IReadOnlyList<AreasResponse>>>
{
    private readonly IAreaRepository _AreaRepository = AreaRepository ?? throw new ArgumentNullException(nameof(AreaRepository));

    public async Task<ErrorOr<IReadOnlyList<AreasResponse>>> Handle(GetAllAreasQuery query, CancellationToken cancellationToken)
    {
        IList<Area> Areas = await _AreaRepository.GetByCompanyId(query.companyId);

        List<AreasResponse> AreasResponse = [];

        if (Areas is not null && Areas.Count > 0)
        {
            foreach (Area assignationType in Areas)
            {
                AreasResponse.Add
                (
                    new AreasResponse
                    (
                        assignationType.Id.Value,

                        assignationType.Name,
                        assignationType.NameEnglish,

                        assignationType.IsFormsVisible
                    )
                );
            }
        }
        
        return AreasResponse;
    }
}