using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Benefits.DeleteBenefitQuery;

public record DeleteBenefitQuery(Guid Id) : IRequest<ErrorOr<bool>>;