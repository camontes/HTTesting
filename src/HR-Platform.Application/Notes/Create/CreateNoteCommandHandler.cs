using ErrorOr;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.NoteFiles;
using HR_Platform.Domain.Notes;
using HR_Platform.Domain.NotificationNotes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Notes.Create;

internal sealed class CreateNoteCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    INoteRepository noteRepository,
    INotificationNoteRepository notificationNoteRepository,
    INoteFileRepository noteFileRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateNoteCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly INoteRepository _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
    private readonly INotificationNoteRepository _notificationNoteRepository = notificationNoteRepository ?? throw new ArgumentNullException(nameof(notificationNoteRepository));
    private readonly INoteFileRepository _noteFileRepository = noteFileRepository ?? throw new ArgumentNullException(nameof(noteFileRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Notes.CreationDate", "CreationDate is not valid");

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(command.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.Validation("Notes.CollaboratorId", "The Collaborator with the provide Id wasn't found");

        Collaborator? collaboratorWhoCreated = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        if (collaboratorWhoCreated is null)
            return Error.Validation("Notes.CollaboratorId", "The Collaborator WHO CREATED with The Provide Id wasn't found");

        List<NoteFile> noteFiles = [];

        Note noteResult = new
        (
            new NoteId(Guid.NewGuid()),
            command.Description, //Description
            collaboratorWhoCreated.Id, //CreatedBy
            oldCollaborator.Id, //AssignedTo
            null, // ParentNoteId
            command.IsPublic,
            true, //IsEditable
            true, //IsDeleletable
            creationDate,
            creationDate
        );

        _noteRepository.Add(noteResult);

        if (command.NotesList is not null && command.NotesList.Count > 0)
        {
            foreach (CreateNotesObjectFile item in command.NotesList)
            {
                NoteFile temp = new
                (
                    new NoteFileId(Guid.NewGuid()),
                    noteResult.Id,
                    item.FileName,
                    item.UrlFile,
                    true, // IsEditable
                    true, // IsDeleteable
                    creationDate,
                    creationDate
                );
                noteFiles.Add(temp);
            }
            _noteFileRepository.AddRangeNoteFiles(noteFiles);
        }

        NotificationNote notification = new
        (
           new NotificationNoteId(Guid.NewGuid()),
           false, //IsRead 
           true, //IsEditable
           true, //IsDeletable
           command.EmailChangeBy == collaboratorWhoCreated.BusinessEmail.Value ? noteResult.AssignedTo : noteResult.CreatedBy, //CollaboratorId
           creationDate,
           creationDate
        );

        _notificationNoteRepository.Add(notification);

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