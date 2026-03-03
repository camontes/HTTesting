using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Collaborators.ChangeState;

internal sealed class ChangeCollaboratorStateCommandHandler(
    ICollaboratorRepository collaboratorRepository,

    IStringService stringService,
    ITimeFormatService timeFormatService,

    IUnitOfWork unitOfWork
    ) : IRequestHandler<ChangeCollaboratorStateCommand, ErrorOr<CollaboratorsResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));

    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<CollaboratorsResponse>> Handle(ChangeCollaboratorStateCommand command, CancellationToken cancellationToken)
    {
        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");


        /* Edit Collaborator */

        CollaboratorId collaboratorId = new(Guid.Parse(command.Id));

        Collaborator? collaborator = await _collaboratorRepository.GetByIdAsync(collaboratorId);

        if (collaborator is null)
            return Error.Validation("Collaborators.Id", "Collaborator not found");

        collaborator.IsSuspended = command.IsSuspended;
        collaborator.SuspensionReason = !string.IsNullOrEmpty(command.SuspensionReason) ? command.SuspensionReason : string.Empty;
        collaborator.CollaboratorStateId = new(command.CollaboratorStateId);

        collaborator.EditionDate = editionDate;

        _collaboratorRepository.Update(collaborator);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        CollaboratorsResponse collaboratorResponse = new
        (
            collaborator.Id.Value,

            collaborator.CompanyId.Value,
            collaborator.RoleId.Value,
            collaborator.DocumentTypeId.Value,
            collaborator.OtherDocumentType,
            collaborator.AssignationId.Value,
            collaborator.CollaboratorStateId.Value,

            collaborator.Document,

            collaborator.DocumentType is not null ? collaborator.DocumentType.Name : string.Empty,
            collaborator.DocumentType is not null ? collaborator.DocumentType.NameEnglish : string.Empty,

            collaborator.Assignation is not null ? collaborator.Assignation.Name : string.Empty,
            collaborator.Assignation is not null ? collaborator.Assignation.NameEnglish : string.Empty,

            collaborator.BusinessEmail is not null ? collaborator.BusinessEmail.Value : string.Empty,
            collaborator.PersonalEmail is not null && collaborator.PersonalEmail.Value is not null ? collaborator.PersonalEmail.Value : string.Empty,

            collaborator.Name,
            _stringService.GetInitials(collaborator.Name),

            collaborator.Role is not null ? collaborator.Role.Name : string.Empty,
            collaborator.Role is not null ? collaborator.Role.NameEnglish : string.Empty,

            !string.IsNullOrEmpty(collaborator.Photo) ? collaborator.Photo : string.Empty,
            !string.IsNullOrEmpty(collaborator.PhotoName) ? collaborator.PhotoName : string.Empty,

            !string.IsNullOrEmpty(collaborator.PhoneNumber) ? collaborator.PhoneNumber : string.Empty,

            collaborator.IsSuspended,
            collaborator.ShowNewFeatures,

            collaborator.SuspensionReason,

            collaborator.EntranceDate.Value,
            _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "dd MMM yyyy", new CultureInfo("es-CO")),
            _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "MMM dd yyyy", new CultureInfo("en-US")),

            _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "dd/MMMM/yyyy", new CultureInfo("es-CO")),
            _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "MMMM/dd/yyyy", new CultureInfo("en-US")),

            collaborator.EditionDate.Value,
            _timeFormatService.GetDateFormatMonthLarge(collaborator.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")),
            _timeFormatService.GetDateFormatMonthLarge(collaborator.EditionDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")),

            _timeFormatService.GetDateTimeFormatMonthToltip(collaborator.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")),
            _timeFormatService.GetDateTimeFormatMonthToltip(collaborator.EditionDate.Value, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US"))
        );

        return collaboratorResponse;
    }
}