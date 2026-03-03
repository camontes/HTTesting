using ErrorOr;
using HR_Platform.Application.Surveys.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Surveys;
using MediatR;

namespace HR_Platform.Application.Surveys.GetByCompanyId;

internal sealed class GetSurveyByCompanyIdQueryHandler
(
    ICompanyRepository companyRepository,
    ISurveyRepository surveyRepository
)
:
IRequestHandler<GetSurveyByCompanyIdQuery, ErrorOr<List<SurveysResponse>>>
{
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly ISurveyRepository _surveyRepository = surveyRepository ?? throw new ArgumentNullException(nameof(surveyRepository));

    public async Task<ErrorOr<List<SurveysResponse>>> Handle(GetSurveyByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<Survey>? surveysBCompany = await _surveyRepository.GetByCompanyIdAsync(oldCompany.Id);

        List<SurveysResponse> response = surveysBCompany is null
                ? []
                : surveysBCompany
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
