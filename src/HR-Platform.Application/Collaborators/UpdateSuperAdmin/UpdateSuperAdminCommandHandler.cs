using ErrorOr;
using HR_Platform.Application.Collaborators.UpdateSuperAdmin;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Assignations.Update;

internal sealed class UpdateSuperAdminCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateSuperAdminCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateSuperAdminCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");
        int iterator = 0;

        if (TimeDate.Create(editionDateString) is not TimeDate editationDate)
            return Error.Validation("BrigadeMembers.Editation", "CreationDate is not valid");

        if (!command.EmailChangeBy.Equals("superadminth@exsis.com.co", StringComparison.CurrentCultureIgnoreCase) && !command.EmailChangeBy.Equals("masterth@exsis.com.co", StringComparison.CurrentCultureIgnoreCase))
            return Error.Forbidden("UpdateSuperAdmin.Email", "You do not have authorization to make changes to this profile.");

        if (await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy) is not Collaborator collaboratorData)
            return Error.Validation("UpdateSuperAdmin", "Collaborator was not found");

        if (collaboratorData is not null)
        {
            if (!string.IsNullOrEmpty(command.Name))
            {
                collaboratorData.Name = command.Name;
                iterator++;
            }

            if (!string.IsNullOrEmpty(command.Phone))
            {
                collaboratorData.PhoneNumber = command.Phone;
                iterator++;
            }

            if (command.IsChangedPhoto)
            {
                collaboratorData.Photo = command.PhotoURL;
                collaboratorData.PhotoName = command.PhotoName;
                iterator++;
            }
        }

        try
        {
            if (iterator > 0 && collaboratorData is not null)
            {
                collaboratorData.EditionDate = editationDate;
                _collaboratorRepository.Update(collaboratorData);
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