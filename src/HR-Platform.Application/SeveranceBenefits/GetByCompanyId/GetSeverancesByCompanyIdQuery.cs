using ErrorOr;
using MediatR;
using HR_Platform.Application.SeveranceBenefits.Common;

namespace HR_Platform.Application.SeveranceBenefits.GetByCompanyId;

public record GetSeveranceBenefitsByCompanyIdQuery(Guid CompanyId, int Page, int PageSize) : IRequest<ErrorOr<SeveranceBenefitWithCountResponse>>;