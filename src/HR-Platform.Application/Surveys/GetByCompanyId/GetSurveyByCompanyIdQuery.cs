using ErrorOr;
using HR_Platform.Application.Surveys.Common;
using MediatR;

namespace HR_Platform.Application.Surveys.GetByCompanyId;

public record GetSurveyByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<List<SurveysResponse>>>;