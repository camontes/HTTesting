using ErrorOr;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Application.Surveys.Common;
using HR_Platform.Domain.SurveyQuestions;
using HR_Platform.Domain.Surveys;
using MediatR;

namespace HR_Platform.Application.Surveys.GetById;

internal sealed class GetSurveyByIdQueryHandler
(
    ISurveyRepository surveyRepository,

    ICalculateTimeDifference calculateTimeDifference,
    IStringService stringService,
    ITimeFormatService timeFormatService
)
:
IRequestHandler<GetSurveyByIdQuery, ErrorOr<SurveyAndQuestionsResponse>>
{
    private readonly ISurveyRepository _surveyRepository = surveyRepository ?? throw new ArgumentNullException(nameof(surveyRepository));

    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<SurveyAndQuestionsResponse>> Handle(GetSurveyByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _surveyRepository.GetByIdAsync(new SurveyId(query.SurveyId)) is not Survey survey)
            return Error.NotFound("Survey.GetSurveyById", "The Survey with the provide id was not found");

        List<SurveyQuestionsResponse> surveyQuestionsResponse = [];

        if (survey is not null && survey.SurveyQuestions.Count > 0)
        {
            foreach (SurveyQuestion surveyQuestion in survey.SurveyQuestions)
            {
                SurveyQuestionsResponse surveyQuestionResponse = new
                (
                    surveyQuestion.Id.Value,

                    surveyQuestion.Text,

                    surveyQuestion.SurveyQuestionType.Id.Value,
                    surveyQuestion.SurveyQuestionType.Name,
                    surveyQuestion.SurveyQuestionType.NameEnglish,

                    surveyQuestion.SurveyQuestionMandatoryType.Id.Value,
                    surveyQuestion.SurveyQuestionMandatoryType.Name,
                    surveyQuestion.SurveyQuestionMandatoryType.NameEnglish
                );

                surveyQuestionsResponse.Add(surveyQuestionResponse);
            }
        }

        SurveyAndQuestionsResponse response = new
        (
            survey.Id.Value,

            survey.CompanyId.Value,

            survey.Name,
            survey.Description,

            survey.IsVisible,

            surveyQuestionsResponse
        );

        return response;
    }
}