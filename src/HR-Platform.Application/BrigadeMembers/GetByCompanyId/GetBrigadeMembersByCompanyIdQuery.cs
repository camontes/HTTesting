using ErrorOr;
using MediatR;
using HR_Platform.Application.BrigadeMembers.Common;

namespace HR_Platform.Application.BrigadeMembers.GetByCompanyId;

public record GetBrigadeMembersByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<BrigadeResponse>>;