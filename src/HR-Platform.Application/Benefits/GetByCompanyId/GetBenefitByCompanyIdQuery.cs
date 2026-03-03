using ErrorOr;
using HR_Platform.Application.Benefits.Common;
using MediatR;

namespace HR_Platform.Application.Benefits.GetByCompanyId;

public record GetBenefitByCompanyIdQuery(Guid CompanyId, bool IsVisible) : IRequest<ErrorOr<List<BenefitFileResponse>>>;