using ErrorOr;
using HR_Platform.Application.CurrencyTypes.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllCurrencyTypesQuery() : IRequest<ErrorOr<IReadOnlyList<CurrencyTypesResponse>>>;