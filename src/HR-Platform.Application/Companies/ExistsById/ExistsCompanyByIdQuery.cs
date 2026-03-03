using ErrorOr;
using HR_Platform.Application.Common;
using MediatR;

namespace HR_Platform.Application.Companies.ExistsById;

public record ExistsCompanyByIdQuery(Guid Id) : IRequest<ErrorOr<BooleanExistsResponse>>;