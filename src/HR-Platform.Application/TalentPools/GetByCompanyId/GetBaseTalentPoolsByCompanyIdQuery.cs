using ErrorOr;
using HR_Platform.Application.TalentPools.Common;
using MediatR;

namespace HR_Platform.Application.TalentPools.GetByCompanyId;

public record GetBaseTalentPoolsByCompanyIdQuery(int Page, int PageSize) : IRequest<ErrorOr<List<TalentPoolsAndCountByCompanyResponse>>>;