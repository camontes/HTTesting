using ErrorOr;
using HR_Platform.Application.Surveys.Common;
using MediatR;

namespace HR_Platform.Application.Surveys.GetById;

public record GetSurveyByIdQuery(Guid SurveyId) : IRequest<ErrorOr<SurveyAndQuestionsResponse>>;