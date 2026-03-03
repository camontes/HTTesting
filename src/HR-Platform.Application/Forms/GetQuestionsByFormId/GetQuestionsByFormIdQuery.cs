using ErrorOr;
using HR_Platform.Application.Forms.Common;
using MediatR;

namespace HR_Platform.Application.Forms.GetByAreaId;

public record GetQuestionsByFormIdQuery(Guid FormId) : IRequest<ErrorOr<FormQuestionsResponse>>;