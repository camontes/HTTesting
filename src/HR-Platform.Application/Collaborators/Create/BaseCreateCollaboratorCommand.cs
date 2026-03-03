using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Collaborators.Create;

public record BaseCreateCollaboratorsCommand(
    int AssignationTypeId,
    string AssignationId,
    int DocumentTypeId,
    bool IsAnotherDocumentType,
    string? OtherDocumentType,
    string? PositionId,

    string BusinessEmail,
    string? PersonalEmail,

    string Name,

    string Document,

    //string? PhoneNumber,
    //string? CellphoneNumber,

    string? StreetAddress,
    //int CountryCode,
    //string? Country,
    //int StateCode,
    //string? State,
    //int CityCode,
    //string? City,
    //string? ZipCode,
    string? ProfessionalCard,
    string? BirthDate,

    IFormFile? CvFile,

    //IFormFile? PhotoFile,

    bool SendNotificationsToPersonalEmail,
    bool IsPendingInvitation,

    //string BirthDate,
    string EntranceDate
) : IRequest<ErrorOr<Guid>>;
