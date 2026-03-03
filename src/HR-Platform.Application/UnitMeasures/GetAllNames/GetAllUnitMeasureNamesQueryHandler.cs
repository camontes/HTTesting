using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.UnitMeasures.Common;
using HR_Platform.Domain.UnitMeasures;
using MediatR;

namespace HR_Platform.Application.UnitMeasures.GetAllNames;

internal sealed class GetAllUnitMeasureNamesQueryHandler : IRequestHandler<GetAllUnitMeasuresNamesQuery, ErrorOr<IReadOnlyList<UnitMeasuresResponse>>>
{
    private readonly IUnitMeasureRepository _unitMeasureRepository;

    public GetAllUnitMeasureNamesQueryHandler
    (
        IUnitMeasureRepository unitMeasureRepository
    )
    {
        _unitMeasureRepository = unitMeasureRepository ?? throw new ArgumentNullException(nameof(unitMeasureRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<UnitMeasuresResponse>>> Handle(GetAllUnitMeasuresNamesQuery query, CancellationToken cancellationToken)
    {
        IList<UnitMeasure> UnitMeasures = await _unitMeasureRepository.GetAll();

        List<UnitMeasuresResponse> UnitMeasuresResponse = [];

        if (UnitMeasures is not null && UnitMeasures.Count > 0)
        {
            foreach (UnitMeasure item in UnitMeasures)
            {
                UnitMeasuresResponse.Add
                (
                    new UnitMeasuresResponse
                    (
                        item.Id.Value.ToString(),
                        item.Name,
                        item.NameEnglish
                    )
                );
            }
        }
        
        return UnitMeasuresResponse;
    }
}