using ErrorOr;
using HR_Platform.Application.Companies.Common;
using MediatR;

namespace HR_Platform.Application.Companies.GetByEmail;

public record GetCompanyByEmailQuery(string Email) : IRequest<ErrorOr<CompaniesResponse>>;