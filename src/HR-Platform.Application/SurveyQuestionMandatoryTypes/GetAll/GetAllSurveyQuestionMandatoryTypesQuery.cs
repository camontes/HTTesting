using ErrorOr;
using HR_Platform.Application.SurveyQuestionMandatoryTypes.Common;
using MediatR;

namespace HR_Platform.Application.SurveyQuestionMandatoryTypes.GetAll;

public record GetAllSurveyQuestionMandatoryTypesQuery() : IRequest<ErrorOr<IReadOnlyList<SurveyQuestionMandatoryTypeResponse>>>;