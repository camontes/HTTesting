using ErrorOr;
using MediatR;
using HR_Platform.Application.Companies.Common;

namespace HR_Platform.Application.Companies.GetAll;

public record GetAllCompaniesQuery() : IRequest<ErrorOr<IReadOnlyList<CompaniesResponse>>>;