using ErrorOr;
using MediatR;
using HR_Platform.Application.Banks.Common;

namespace HR_Platform.Application.Banks.GetByCompanyId;

public record GetBanksByCompanyIdQuery(Guid CompanyId, int Page, int PageSize) : IRequest<ErrorOr<BanksAndCountByCompanyResponse>>;