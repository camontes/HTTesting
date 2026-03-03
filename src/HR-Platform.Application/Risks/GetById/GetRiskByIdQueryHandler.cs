using ErrorOr;
using HR_Platform.Application.Risks.Common;
using HR_Platform.Application.Risks.GetById;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Risks;
using MediatR;

namespace HR_Platform.Application.Risks.GetByCollaboratorId;

internal sealed class GetRiskByIdQueryHandler(
    IRiskRepository riskRepository,
    ICalculateTimeDifference calculateTimeDifference
    ) : IRequestHandler<GetRiskByIdQuery, ErrorOr<RiskResponse>>
{
    private readonly IRiskRepository _riskRepository = riskRepository ?? throw new ArgumentNullException(nameof(riskRepository));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<RiskResponse>> Handle(GetRiskByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _riskRepository.GetByIdAsync(new RiskId(query.Id)) is not Risk oldRisk)
        {
            return Error.NotFound("Risk.NotFound", "The Risk with the provide Id was not found.");
        }

        RiskResponse response = new
        (
            oldRisk.Id.Value.ToString(),
            oldRisk.Name,
            oldRisk.Description,
            oldRisk.ImageName,
            oldRisk.ImageURL,
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldRisk.ImageCreationTime.Value).Split('.')[0]), // TimePosted
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldRisk.ImageCreationTime.Value).Split('.')[1]), // TimePostedEnglish
            oldRisk.VideoName,
            oldRisk.VideoURL,
            oldRisk.IsVisible,
            oldRisk.EditionDate.Value,
            oldRisk.CreationDate.Value
        );

        return response;

    }
}