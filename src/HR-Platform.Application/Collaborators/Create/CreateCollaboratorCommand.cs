using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Collaborators.Create;

public record CreateCollaboratorsCommand(
    string CompanyId,
    string EmailChangeBy,

    int AssignationTypeId,
    string AssignationId,
    int DocumentTypeId,
    bool IsAnotherDocumentType,
    string OtherDocumentType,

    string? PositionId,

    string BusinessEmail,
    string PersonalEmail,

    string Name,

    string Document,
    string? ProfessionalCard,
    string? BirthDate, 

    //string PhoneNumber,
    //string CellphoneNumber,

    string StreetAddress,
    //int CountryCode,
    //string Country,
    //int StateCode,
    //string State,
    //int CityCode,
    //string City,
    //string ZipCode,

    string CvFile,
    string CvName,
    string Photo,
    string PhotoName,

    bool SendNotificationsToPersonalEmail,
    bool IsPendingInvitation,

    //string BirthDate,
    string EntranceDate
) : IRequest<ErrorOr<Guid>>;
