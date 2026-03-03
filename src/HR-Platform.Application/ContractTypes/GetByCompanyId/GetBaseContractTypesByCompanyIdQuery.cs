using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetBaseContractTypesByCompanyIdQuery(int Page, int PageSize) : IRequest<ErrorOr<List<ContractTypesAndCountByCompanyResponse>>>;