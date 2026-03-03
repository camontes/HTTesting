using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.UpdateLocation;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.EconomicLevels;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Assignations.Update;

internal sealed class UpdateLocationCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateLocationCommand, ErrorOr<UpdateLocationResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<UpdateLocationResponse>> Handle(UpdateLocationCommand command, CancellationToken cancellationToken)
    {
        TimeZoneInfo colombiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, colombiaTimeZone);

        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(command.CollaboratorId)) is not Collaborator oldCollaborator)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");
        }

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        if (command.Birthdate is not null)
        {
            string? dateString = command.Birthdate?.ToString("MM/dd/yyyy HH:mm:ss");

            if (TimeDate.Create(dateString) is not TimeDate birthDate)
                return Error.Validation("Collaborators.BirthDate", "BirthDate is not valid");
            oldCollaborator.Birthdate = birthDate;
        }

        if (!string.IsNullOrEmpty(command.Country))
        {
            oldCollaborator.Country = command.Country;
        }

        if (!string.IsNullOrEmpty(command.Department))
        {
            oldCollaborator.Department = command.Department;
        }

        if (!string.IsNullOrEmpty(command.City))
        {
            oldCollaborator.City = command.City;
        }

        if (!string.IsNullOrEmpty(command.LocationAddress))
        {
            oldCollaborator.LocationAddress = command.LocationAddress;
        }

        if (!string.IsNullOrEmpty(command.EconomicLevelId))
        {
            if (command.EconomicLevelId != "Ninguno")
            {
                oldCollaborator.EconomicLevelId = new EconomicLevelId(int.Parse(command.EconomicLevelId));
            }
            else
            {
                oldCollaborator.EconomicLevelId = new EconomicLevelId(1);
            }
        }

        if (!string.IsNullOrEmpty(command.PhoneNumber))
        {
            oldCollaborator.PhoneNumber = command.PhoneNumber;
        }

        if (!string.IsNullOrEmpty(command.PostalCode))
        {
            oldCollaborator.PostalCode = command.PostalCode;
        }


        if (command.Birthdate is not null ||
            !string.IsNullOrEmpty(command.Country) ||
            !string.IsNullOrEmpty(command.Department) ||
            !string.IsNullOrEmpty(command.City) ||
            !string.IsNullOrEmpty(command.LocationAddress) ||
            !string.IsNullOrEmpty(command.EconomicLevelId) ||
            !string.IsNullOrEmpty(command.PhoneNumber) ||
            !string.IsNullOrEmpty(command.PostalCode))
        {
            oldCollaborator.ChangedBy = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Role.Name : oldCollaborator.ChangedBy;
            oldCollaborator.EmailChangedBy = command.EmailChangeBy;
            oldCollaborator.EditionDate = editionDate;
            _collaboratorRepository.Update(oldCollaborator);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        UpdateLocationResponse response = new(
            command.CollaboratorId,
            command.CompanyId,
            command.Birthdate,
            command.Country,
            command.Department,
            command.City,
            command.EconomicLevelId,
            command.LocationAddress,
            command.PhoneNumber,
            command.PostalCode,
            editionDate
            );

        return response;
    }
}