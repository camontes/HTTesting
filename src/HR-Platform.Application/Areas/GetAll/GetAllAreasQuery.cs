using ErrorOr;
using HR_Platform.Application.Areas.Common;
using HR_Platform.Domain.Companies;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllAreasQuery(CompanyId companyId) : IRequest<ErrorOr<IReadOnlyList<AreasResponse>>>;