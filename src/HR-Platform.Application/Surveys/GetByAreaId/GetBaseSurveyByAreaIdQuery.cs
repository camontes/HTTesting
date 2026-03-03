using ErrorOr;
using HR_Platform.Application.Surveys.Common;
using MediatR;

namespace HR_Platform.Application.Surveys.GetByAreaId;

public record GetBaseSurveyByAreaIdQuery(Guid AreaId) : IRequest<ErrorOr<List<SurveysResponse>>>;