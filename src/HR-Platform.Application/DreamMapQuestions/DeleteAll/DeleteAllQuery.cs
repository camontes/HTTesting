using ErrorOr;
using MediatR;

namespace HR_Platform.Application.DreamMapQuestions.DeleteAll;

public record DeleteAllQuery(Guid CompanyId) : IRequest<ErrorOr<bool>>;