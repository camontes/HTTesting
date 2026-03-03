using ErrorOr;
using MediatR;
using HR_Platform.Application.SurveyQuestionMandatoryTypes.Common;
using HR_Platform.Application.SurveyQuestionMandatoryTypes.GetAll;
using HR_Platform.Domain.SurveyQuestionMandatoryTypes;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllSurveyQuestionMandatoryTypesQueryHandler(
    ISurveyQuestionMandatoryTypeRepository surveyQuestionMandatoryTypeRepository
    ) : IRequestHandler<GetAllSurveyQuestionMandatoryTypesQuery, ErrorOr<IReadOnlyList<SurveyQuestionMandatoryTypeResponse>>>
{
    private readonly ISurveyQuestionMandatoryTypeRepository _surveyQuestionMandatoryTypeRepository =
        surveyQuestionMandatoryTypeRepository ?? throw new ArgumentNullException(nameof(surveyQuestionMandatoryTypeRepository));

    public async Task<ErrorOr<IReadOnlyList<SurveyQuestionMandatoryTypeResponse>>> Handle(GetAllSurveyQuestionMandatoryTypesQuery query, CancellationToken cancellationToken)
    {
        IList<SurveyQuestionMandatoryType> surveyQuestionMandatoryTypes = await _surveyQuestionMandatoryTypeRepository.GetAll();

        List<SurveyQuestionMandatoryTypeResponse> surveyQuestionMandatoryTypeResponses = [];

        if (surveyQuestionMandatoryTypes is not null && surveyQuestionMandatoryTypes.Count > 0)
        {
            foreach (SurveyQuestionMandatoryType surveyQuestionMandatoryType in surveyQuestionMandatoryTypes)
            {
                surveyQuestionMandatoryTypeResponses.Add
                (
                    new SurveyQuestionMandatoryTypeResponse
                    (
                        surveyQuestionMandatoryType.Id.Value,

                        surveyQuestionMandatoryType.Name,
                        surveyQuestionMandatoryType.NameEnglish
                    )
                );
            }
        }

        return surveyQuestionMandatoryTypeResponses;
    }
}