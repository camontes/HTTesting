using ErrorOr;
using HR_Platform.Application.WorkplaceEvidences.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using HR_Platform.Domain.WorkplaceEvidences;
using MediatR;

namespace HR_Platform.Application.WorkplaceEvidences.Create;

internal sealed class CreateWorkplaceEvidencesCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IWorkplaceEvidenceRepository WorkplaceEvidenceRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateWorkplaceEvidencesCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IWorkplaceEvidenceRepository _workplaceEvidenceRepository = WorkplaceEvidenceRepository ?? throw new ArgumentNullException(nameof(WorkplaceEvidenceRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateWorkplaceEvidencesCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");
        Collaborator? oldCollaborator = null;

        if (!string.IsNullOrEmpty(command.CollaboratorId))
        {
            oldCollaborator = await _collaboratorRepository.GetByIdAsync(new CollaboratorId(Guid.TryParse(command.CollaboratorId, out Guid result) ? result : Guid.NewGuid()));
        }
        else
        {
            oldCollaborator = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);
        }

        if (oldCollaborator == null)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("WorkplaceEvidences.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<WorkplaceEvidence> workplaceEvidenceList = [];

        foreach (FileWorkplaceEvidenceFormatResponse item in command.FormatFiles)
        {
            WorkplaceEvidence result = new
            (
                new WorkplaceEvidenceId(Guid.NewGuid()),
                oldCollaborator.Id,
                item.FileName,
                item.FileURL,
                command.EmailChangeBy,
                CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty,
                CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Photo : string.Empty,
                true, //IsEditable
                true, //IsDeletable
                creationDate,
                creationDate
            );
            workplaceEvidenceList.Add(result);
        }
        if (workplaceEvidenceList.Count > 0)
        {
            _workplaceEvidenceRepository.AddRange(workplaceEvidenceList);
        }

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}