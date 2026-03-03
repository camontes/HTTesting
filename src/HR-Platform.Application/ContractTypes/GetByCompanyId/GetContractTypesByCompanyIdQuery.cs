using ErrorOr;
using MediatR;
using HR_Platform.Application.ContractTypes.Common;

namespace HR_Platform.Application.ContractTypes.GetByCompanyId;

public record GetContractTypesByCompanyIdQuery(Guid CompanyId, int Page, int PageSize) : IRequest<ErrorOr<ContractTypesAndCountByCompanyResponse>>;