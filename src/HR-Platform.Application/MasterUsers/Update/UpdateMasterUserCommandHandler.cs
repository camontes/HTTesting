using ErrorOr;
using HR_Platform.Domain.MasterUsers;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.MasterUsers.Update;

internal sealed class UpdateMasterUserCommandHandler : IRequestHandler<UpdateMasterUserCommand, ErrorOr<bool>>
{
    private readonly IMasterUserRepository _masterUserRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMasterUserCommandHandler
    (
        IMasterUserRepository masterUserRepository,
        IUnitOfWork unitOfWork
    )
    {
        _masterUserRepository = masterUserRepository ?? throw new ArgumentNullException(nameof(masterUserRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<bool>> Handle(UpdateMasterUserCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");
        int iterator = 0;

        if (TimeDate.Create(editionDateString) is not TimeDate editationDate)
            return Error.Validation("BrigadeMembers.Editation", "CreationDate is not valid");

        if (!command.EmailChangeBy.Equals("masterth@exsis.com.co", StringComparison.CurrentCultureIgnoreCase))
            return Error.Forbidden("UpdateSuperAdmin.Email", "You do not have authorization to make changes to this profile.");

        if (PhoneNumber.Create(command.Phone) is not PhoneNumber phoneNumber)
            return Error.Validation("Collaborators.PhoneNumber", "PhoneNumber has not valid format");

        if (await _masterUserRepository.GetByEmailAsync(command.EmailChangeBy) is not MasterUser oldMasterUser)
            return Error.Validation("UpdateMaster", "Master was not found");

        if (oldMasterUser is not null)
        {
            if (!string.IsNullOrEmpty(command.Name))
            {
                oldMasterUser.Name = command.Name;
                iterator++;
            }

            if (!string.IsNullOrEmpty(command.Phone))
            {
                oldMasterUser.PhoneNumber = phoneNumber;
                iterator++;
            }

            if (command.IsChangedPhoto)
            {
                oldMasterUser.Photo = command.PhotoURL;
                oldMasterUser.PhotoName = command.PhotoName;
                iterator++;
            }
        }

        try
        {
            if (iterator > 0 && oldMasterUser is not null)
            {
                oldMasterUser.EditionDate = editationDate;
                _masterUserRepository.Update(oldMasterUser);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return false;
        }
        catch (Exception)
        {

            return false;
        }
    }

}