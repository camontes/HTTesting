using ErrorOr;
using HR_Platform.Application.TypeAccounts.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetBaseTypeAccountsByCompanyIdQuery(int Page, int PageSize) : IRequest<ErrorOr<List<TypeAccountsAndCountByCompanyResponse>>>;