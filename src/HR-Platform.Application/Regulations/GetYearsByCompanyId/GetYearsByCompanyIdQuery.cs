using ErrorOr;
using HR_Platform.Application.Regulations.Common;
using MediatR;

namespace HR_Platform.Application.Regulations.GetYearsByCompanyId;

public record GetYearsByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<RegulationFileYearsListResponse>>;

