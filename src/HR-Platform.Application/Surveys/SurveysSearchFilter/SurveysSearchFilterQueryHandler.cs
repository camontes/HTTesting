using ErrorOr;
using HR_Platform.Application.SearchFilters.Common;
using HR_Platform.Application.Surveys.Common;
using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.SearchFilters;
using HR_Platform.Domain.Surveys;
using MediatR;

namespace HR_Platform.Application.Surveys.SurveysSearchFilter;

internal sealed class SurveysSearchFilterQueryHandler
(
    IAreaRepository areaRepository,
    ISurveyRepository surveyRepository
):
IRequestHandler<SurveysSearchFilterQuery, ErrorOr<SearchFilterResponse>>
{
    private readonly IAreaRepository _areaRepository = areaRepository ?? throw new ArgumentNullException(nameof(areaRepository));
    private readonly ISurveyRepository _surveyRepository = surveyRepository ?? throw new ArgumentNullException(nameof(surveyRepository));

    public async Task<ErrorOr<SearchFilterResponse>> Handle(SurveysSearchFilterQuery query, CancellationToken cancellationToken)
    {
        var results = await _surveyRepository.SearchAsync
        (
            new BasicRequestSearch
            {
                Query = query.Query,
                Page = query.Page,
                PageSize = query.PageSize,
                CompanyId = new CompanyId(query.CompanyId),
                AreaId = new AreaId(query.AreaId)
            }
        );

        if (await _areaRepository.GetByIdAsync(new AreaId(query.AreaId)) is not Area oldArea)
            return Error.NotFound("Area.NotFound", "The Area with the provide Id was not found.");

        List<Survey>? surveysByArea = results.Items.ToList();

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

        return new SearchFilterResponse
        (
            results.TotalCount,
            surveysByArea is not null && surveysByArea.Count > 0 ? surveysByArea : [],
            results.TotalCount > 0 ? "Resultados encontrados." : "No se encontraron resultados."
        );
    }
}