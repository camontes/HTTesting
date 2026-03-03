using ErrorOr;
using HR_Platform.Application.Companies.Common;
using MediatR;

namespace HR_Platform.Application.Companies.GetById;

public record GetCompanyByIdQuery(Guid Id) : IRequest<ErrorOr<CompaniesResponse>>;