using ErrorOr;
using MediatR;
using HR_Platform.Application.TypeAccounts.Common;

namespace HR_Platform.Application.TypeAccounts.GetByCompanyId;

public record GetTypeAccountsByCompanyIdQuery(Guid CompanyId, int Page, int PageSize) : IRequest<ErrorOr<TypeAccountsAndCountByCompanyResponse>>;