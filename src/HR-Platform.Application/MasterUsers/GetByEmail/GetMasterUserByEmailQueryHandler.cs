using ErrorOr;
using HR_Platform.Application.MasterUsers.Common;
using HR_Platform.Domain.MasterUsers;
using MediatR;

namespace HR_Platform.Application.MasterUsers.GetByEmail;

internal sealed class GetMasterUserByEmailQueryHandler(IMasterUserRepository masterUserRepository) : IRequestHandler<GetMasterUserByEmailQuery, ErrorOr<MasterUsersResponse>>
{
    private readonly IMasterUserRepository _masterUserRepository = masterUserRepository ?? throw new ArgumentNullException(nameof(masterUserRepository));

    public async Task<ErrorOr<MasterUsersResponse>> Handle(GetMasterUserByEmailQuery query, CancellationToken cancellationToken)
    {
        if (await _masterUserRepository.GetByEmailAsync(query.Email) is not MasterUser masterUser)
        {
            return Error.NotFound("MasterUser.NotFound", "The master user with the provide Email was not found.");
        }

        return new MasterUsersResponse
        (
            masterUser.Id.Value,

            masterUser.Email.Value,

            masterUser.Name,
            masterUser.NameEnglish,

            masterUser.PhoneNumber.Value,

            masterUser.Photo,
            masterUser.PhotoName,

            masterUser.RoleName,
            masterUser.RoleNameEnglish,

            masterUser.LoginCode
        );
    }
}