using ErrorOr;
using HR_Platform.Application.Surveys.Common;
using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Surveys;
using MediatR;

namespace HR_Platform.Application.Surveys.GetByAreaId;

internal sealed class GetSurveyByAreaIdQueryHandler
(
    IAreaRepository areaRepository,
    ISurveyRepository surveyRepository
)
:
IRequestHandler<GetSurveyByAreaIdQuery, ErrorOr<List<SurveysResponse>>>
{
    private readonly IAreaRepository _areaRepository = areaRepository ?? throw new ArgumentNullException(nameof(areaRepository));
    private readonly ISurveyRepository _surveyRepository = surveyRepository ?? throw new ArgumentNullException(nameof(surveyRepository));

    public async Task<ErrorOr<List<SurveysResponse>>> Handle(GetSurveyByAreaIdQuery query, CancellationToken cancellationToken)
    {
        if (await _areaRepository.GetByIdAsync(new AreaId(query.AreaId)) is not Area oldArea)
            return Error.NotFound("Area.NotFound", "The Area with the provide Id was not found.");

        List<Survey>? surveysByArea = await _surveyRepository.GetByAreaAndCompanyIdAsync(oldArea.Id, new CompanyId(query.CompanyId));

        List<SurveysResponse> response = surveysByArea is null
                ? []
                : surveysByArea
                .Select
                (
                    s => new SurveysResponse
                    (
                        s.Id.Value,

                        query.CompanyId,

                        s.Name,
                        s.Description,

                        s.IsVisible, // IsVisibleWithEye

                        s.EmailWhoChangedByTH,
                        s.NameWhoChangedByTH,

                        s.CreationDate.Value,
                        s.EditionDate.Value
                    )
                )
                .ToList();

        return response.OrderByDescending(t => t.CreationTime).ToList();
    }
}
