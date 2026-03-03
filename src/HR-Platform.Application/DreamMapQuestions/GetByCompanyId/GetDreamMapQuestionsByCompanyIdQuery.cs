using ErrorOr;
using MediatR;
using HR_Platform.Application.DreamMapQuestions.Common;

namespace HR_Platform.Application.DreamMapQuestions.GetByCompanyId;

public record GetDreamMapQuestionsByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<List<DreamMapQuestionsResponse>>>;