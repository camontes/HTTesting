using ErrorOr;
using HR_Platform.Application.QuestionTypes.Common;
using MediatR;

namespace HR_Platform.Application.QuestionTypes.GetByCompanyId;

public record GetQuestionTypesByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<List<QuestionTypesResponse>>>;