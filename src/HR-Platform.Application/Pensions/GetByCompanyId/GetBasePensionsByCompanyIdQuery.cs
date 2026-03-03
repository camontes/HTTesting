using ErrorOr;
using HR_Platform.Application.Pensions.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetBasePensionsByCompanyIdQuery(int Page, int PageSize) : IRequest<ErrorOr<List<PensionsAndCountByCompanyResponse>>>;