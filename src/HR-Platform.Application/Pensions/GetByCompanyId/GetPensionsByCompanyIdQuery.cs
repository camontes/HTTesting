using ErrorOr;
using MediatR;
using HR_Platform.Application.Pensions.Common;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

public record GetPensionsByCompanyIdQuery(Guid CompanyId, int Page, int PageSize) : IRequest<ErrorOr<PensionsAndCountByCompanyResponse>>;