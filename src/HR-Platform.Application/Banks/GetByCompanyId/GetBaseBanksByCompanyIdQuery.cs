using ErrorOr;
using HR_Platform.Application.Banks.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetBaseBanksByCompanyIdQuery(int Page, int PageSize) : IRequest<ErrorOr<List<BanksAndCountByCompanyResponse>>>;