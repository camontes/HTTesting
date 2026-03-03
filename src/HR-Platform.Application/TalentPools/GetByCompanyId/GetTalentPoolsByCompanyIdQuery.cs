using ErrorOr;
using MediatR;
using HR_Platform.Application.TalentPools.Common;

namespace HR_Platform.Application.TalentPools.GetByCompanyId;

public record GetTalentPoolsByCompanyIdQuery(Guid CompanyId, int Page, int PageSize) : IRequest<ErrorOr<TalentPoolsAndCountByCompanyResponse>>;