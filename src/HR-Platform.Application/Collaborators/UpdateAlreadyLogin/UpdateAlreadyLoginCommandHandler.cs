using ErrorOr;
using MediatR;
using HR_Platform.Domain.ValueObjects;
using HR_Platform.Domain.Primitives;
using HR_Platform.Application.Collaborators.UpdateAlreadyLogin;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.ServicesInterfaces;
using System.Globalization;

namespace HR_Platform.Application.Assignations.Update;

internal sealed class UpdateAlreadyLoginCommandHandler(
    ICollaboratorRepository collaboratorRepository,

    IStringService stringService,
    ITimeFormatService timeFormatService,

    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateAlreadyLoginCommand, ErrorOr<CollaboratorsResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));

    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<CollaboratorsResponse>> Handle(UpdateAlreadyLoginCommand query, CancellationToken cancellationToken)
    {
        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(query.Id)) is not Collaborator oldCollaborator)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        oldCollaborator.EditionDate = editionDate;
        oldCollaborator.AlreadyLogin = query.AlreadyLogin;
        oldCollaborator.IsPendingInvitation= false;

        _collaboratorRepository.Update(oldCollaborator);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        DateTime entranceDate = oldCollaborator.EntranceDate != null ? oldCollaborator.EntranceDate.Value : new DateTime();

        CollaboratorsResponse collaboratorsResponse = new
        (
            query.Id,
            oldCollaborator.CompanyId != null ? oldCollaborator.CompanyId.Value : Guid.Empty,
            oldCollaborator.RoleId != null ? oldCollaborator.RoleId.Value : Guid.Empty,
            oldCollaborator.DocumentTypeId != null ? oldCollaborator.DocumentTypeId.Value : 0,
            oldCollaborator.DocumentTypeId != null ? oldCollaborator.OtherDocumentType : string.Empty,

            oldCollaborator.AssignationId != null ? oldCollaborator.AssignationId.Value : Guid.Empty,

            oldCollaborator.CollaboratorStateId.Value,

            oldCollaborator.Document,
            oldCollaborator.DocumentType != null ? oldCollaborator.DocumentType.Name : string.Empty,
            oldCollaborator.DocumentType != null ? oldCollaborator.DocumentType.NameEnglish : string.Empty,

            oldCollaborator.Assignation != null ? oldCollaborator.Assignation.Name : string.Empty,
            oldCollaborator.Assignation != null ? oldCollaborator.Assignation.NameEnglish : string.Empty,

            oldCollaborator.BusinessEmail != null ? oldCollaborator.BusinessEmail.Value : string.Empty,
            oldCollaborator.PersonalEmail != null ? oldCollaborator.PersonalEmail.Value : string.Empty,

            oldCollaborator.Name,
            _stringService.GetInitials(oldCollaborator.Name),

            oldCollaborator.Role != null ? oldCollaborator.Role.Name : string.Empty,
            oldCollaborator.Role != null ? oldCollaborator.Role.NameEnglish : string.Empty,

            !string.IsNullOrEmpty(oldCollaborator.Photo) ? oldCollaborator.Photo : string.Empty,
            !string.IsNullOrEmpty(oldCollaborator.PhotoName) ? oldCollaborator.PhotoName : string.Empty,

            !string.IsNullOrEmpty(oldCollaborator.PhoneNumber) ? oldCollaborator.PhoneNumber : string.Empty,

            oldCollaborator.IsSuspended,
            oldCollaborator.ShowNewFeatures,

            oldCollaborator.SuspensionReason,

            entranceDate,
            _timeFormatService.GetDateFormatMonthShort(entranceDate, "dd MMM yyyy", new CultureInfo("es-CO")),
            _timeFormatService.GetDateFormatMonthShort(entranceDate, "MMM dd yyyy", new CultureInfo("en-US")),

            _timeFormatService.GetDateFormatMonthShort(entranceDate, "dd/MMMM/yyyy", new CultureInfo("es-CO")),
            _timeFormatService.GetDateFormatMonthShort(entranceDate, "MMMM/dd/yyyy", new CultureInfo("en-US")),

            oldCollaborator.EditionDate.Value,
            _timeFormatService.GetDateFormatMonthLarge(oldCollaborator.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")),
            _timeFormatService.GetDateFormatMonthLarge(oldCollaborator.EditionDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")),

            _timeFormatService.GetDateTimeFormatMonthToltip(oldCollaborator.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")),
            _timeFormatService.GetDateTimeFormatMonthToltip(oldCollaborator.EditionDate.Value, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US"))
        );

        return collaboratorsResponse;
    }
}