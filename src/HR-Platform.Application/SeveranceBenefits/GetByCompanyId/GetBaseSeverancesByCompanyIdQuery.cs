using ErrorOr;
using HR_Platform.Application.SeveranceBenefits.Common;
using MediatR;

namespace HR_Platform.Application.SeveranceBenefits.GetByCompanyId;
public record GetBaseSeverancesByCompanyIdQuery (int Page, int PageSize) : IRequest<ErrorOr<List<SeveranceBenefitWithCollaboratorCountResponse>>>;

