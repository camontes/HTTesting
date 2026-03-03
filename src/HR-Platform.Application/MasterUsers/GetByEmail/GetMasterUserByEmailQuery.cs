using ErrorOr;
using HR_Platform.Application.MasterUsers.Common;
using MediatR;

namespace HR_Platform.Application.MasterUsers.GetByEmail;

public record GetMasterUserByEmailQuery(string Email) : IRequest<ErrorOr<MasterUsersResponse>>;