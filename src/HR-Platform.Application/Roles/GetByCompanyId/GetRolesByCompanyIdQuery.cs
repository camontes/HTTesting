using ErrorOr;
using MediatR;
using HR_Platform.Application.Roles.Common;

namespace HR_Platform.Application.Roles.GetByCompanyId;

public record GetRolesByCompanyIdQuery(Guid CompanyId, string RoleName) : IRequest<ErrorOr<List<RolesResponse>>>;