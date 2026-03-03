using ErrorOr;
using HR_Platform.Application.Surveys.Common;
using MediatR;

namespace HR_Platform.Application.Surveys.GetByAreaId;

public record GetSurveyByAreaIdQuery(Guid CompanyId, Guid AreaId) : IRequest<ErrorOr<List<SurveysResponse>>>;