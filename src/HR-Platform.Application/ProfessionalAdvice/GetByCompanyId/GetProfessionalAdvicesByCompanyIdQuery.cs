using ErrorOr;
using MediatR;
using HR_Platform.Application.ProfessionalAdvices.Common;

namespace HR_Platform.Application.ProfessionalAdvices.GetByCompanyId;

public record GetProfessionalAdvicesByCompanyIdQuery(Guid CompanyId, int Page, int PageSize) : IRequest<ErrorOr<ProfessionalAdvicesAndCountByCompanyResponse>>;