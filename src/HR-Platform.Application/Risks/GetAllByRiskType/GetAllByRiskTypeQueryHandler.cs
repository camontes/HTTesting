using ErrorOr;
using HR_Platform.Application.Risks.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Risks;
using HR_Platform.Domain.RiskTypeMains;
using MediatR;

namespace HR_Platform.Application.Risks.GetAllByRiskType;

internal sealed class GetAllByRiskTypeQueryHandler(
    IRiskRepository riskRepository,
    IRiskTypeMainRepository riskTypeMainRepository,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetAllByRiskTypeQuery, ErrorOr<List<RiskResponse>>>
{
    private readonly IRiskRepository _riskRepository = riskRepository ?? throw new ArgumentNullException(nameof(riskRepository));
    private readonly IRiskTypeMainRepository _riskTypeMainRepository = riskTypeMainRepository ?? throw new ArgumentNullException(nameof(riskTypeMainRepository));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<List<RiskResponse>>> Handle(GetAllByRiskTypeQuery query, CancellationToken cancellationToken)
    {
        if (await _riskTypeMainRepository.GetByIdAsync(new RiskTypeMainId(query.Id)) is not RiskTypeMain oldRiskType)
            return Error.NotFound("RiskTypeMainId.NotFound", "The Risk Type with the provide Id was not found.");

        List<Risk> riskList = await _riskRepository.GetRiskByRiskTypeIdAsync(oldRiskType.Id);
        List<RiskResponse> resultOnlyTrue = [];
        List<RiskResponse> resulAll = [];

        if (riskList is not null && riskList.Count != 0)
        {
            foreach (Risk item in riskList)
            {
                    RiskResponse response = new
                    (
                        item.Id.Value.ToString(),
                        item.Name,
                        item.Description,
                        item.ImageName,
                        item.ImageURL,
                        String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.ImageCreationTime.Value).Split('.')[0]), // TimePosted
                        String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.ImageCreationTime.Value).Split('.')[1]), // TimePostedEnglish
                        item.VideoName,
                        item.VideoURL,
                        item.IsVisible,
                        item.EditionDate.Value,
                        item.CreationDate.Value
                    );
                
                if (item.IsVisible)
                {
                    resultOnlyTrue.Add(response);
                }

                resulAll.Add(response);
            }
        }
        return query.IsVisible ? resulAll.OrderByDescending(e => e.EditionDate > e.CreatedDate ? e.EditionDate : e.CreatedDate).ToList() : [.. resultOnlyTrue.OrderByDescending(e => e.EditionDate > e.CreatedDate ? e.EditionDate : e.CreatedDate)];

    }
}