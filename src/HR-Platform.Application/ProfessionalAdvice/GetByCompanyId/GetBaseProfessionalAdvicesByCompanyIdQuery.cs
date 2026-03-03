using ErrorOr;
using HR_Platform.Application.ProfessionalAdvices.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetBaseProfessionalAdvicesByCompanyIdQuery(int Page, int PageSize) : IRequest<ErrorOr<List<ProfessionalAdvicesAndCountByCompanyResponse>>>;