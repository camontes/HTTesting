using ErrorOr;
using MediatR;
using HR_Platform.Application.EducationalLevels.Common;

namespace HR_Platform.Application.EducationalLevels.GetByCompanyId;

public record GetEducationalLevelsByCompanyIdQuery(Guid CompanyId, int Page, int PageSize) : IRequest<ErrorOr<EducationalLevelsAndCountByCompanyResponse>>;