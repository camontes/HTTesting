using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Collaborators.UpdateBasicInformation;

internal sealed class UpdateBasicInformationCommandHandler(
    ICollaboratorRepository collaboratorRepository,

    IStringService stringService,
    ITimeFormatService timeFormatService,

    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateBasicInformationCommand, ErrorOr<UpdateBasicInformationResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));

    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<UpdateBasicInformationResponse>> Handle(UpdateBasicInformationCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(command.Id)) is not Collaborator oldCollaborator)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        oldCollaborator.Photo = !string.IsNullOrEmpty(command.PhotoURL) ? command.PhotoURL : string.Empty;

        Collaborator? collaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        oldCollaborator.ChangedBy = collaboratorWhoChanged is not null ? collaboratorWhoChanged.Role.Name : oldCollaborator.ChangedBy;
        oldCollaborator.EmailChangedBy = command.EmailChangeBy;
        oldCollaborator.EditionDate = editionDate;

        //oldCollaborator.DocumentType = null;

        _collaboratorRepository.Update(oldCollaborator);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        UpdateBasicInformationResponse response = new(
            oldCollaborator.Id.Value,

            !string.IsNullOrEmpty(oldCollaborator.Document) ? oldCollaborator.Document : string.Empty,

            oldCollaborator.DocumentTypeId.Value,
            oldCollaborator.OtherDocumentType,
            oldCollaborator.DocumentType != null ? oldCollaborator.DocumentType.Name : string.Empty,
            oldCollaborator.DocumentType != null ? oldCollaborator.DocumentType.NameEnglish : string.Empty,

            oldCollaborator.BusinessEmail != null && !string.IsNullOrEmpty(oldCollaborator.BusinessEmail.Value) ? oldCollaborator.BusinessEmail.Value : string.Empty,

            !string.IsNullOrEmpty(oldCollaborator.Name) ? oldCollaborator.Name : string.Empty,
            !string.IsNullOrEmpty(oldCollaborator.Name) ? _stringService.GetInitials(oldCollaborator.Name) : string.Empty,

            !string.IsNullOrEmpty(oldCollaborator.Photo) ? oldCollaborator.Photo : string.Empty,

            _timeFormatService.GetDateFormatMonthShort(oldCollaborator.EditionDate.Value, "dd MMM yyyy", new CultureInfo("es-CO")),
            _timeFormatService.GetDateFormatMonthShort(oldCollaborator.EditionDate.Value, "MMM dd yyyy", new CultureInfo("en-US"))
        );

        return response;
    }
}