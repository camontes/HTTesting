using ErrorOr;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.NoteFiles;
using HR_Platform.Domain.Notes;
using HR_Platform.Domain.NotificationNotes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Notes.Update;

internal sealed class UpdateNoteCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    INoteRepository noteRepository,
    INotificationNoteRepository notificationNoteRepository,
    INoteFileRepository noteFileRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateNoteCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly INoteRepository _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
    private readonly INotificationNoteRepository _notificationNoteRepository = notificationNoteRepository ?? throw new ArgumentNullException(nameof(notificationNoteRepository));
    private readonly INoteFileRepository _noteFileRepository = noteFileRepository ?? throw new ArgumentNullException(nameof(noteFileRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateNoteCommand command, CancellationToken cancellationToken)
    {
        DateTime colombianHour = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = colombianHour.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Notes.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(creationDateString) is not TimeDate editionDate)
            return Error.Validation("Notes.CreationDate", "EditionDate is not valid");

        if (await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy) is not Collaborator oldCollaborator)
            return Error.Validation("Notes.CollaboratorId", "The Collaborator with the provide Id wasn't found");

        Collaborator? collaboratorWhoCreated = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        if (collaboratorWhoCreated is null)
            return Error.Validation("Notes.CollaboratorId", "The Collaborator WHO CREATED with The Provide Id wasn't found");


        List<Note> notes = [];
        List<NoteFile> noteFiles = [];

        Note? oldNote = await _noteRepository.GetByIdAsync(new NoteId(command.Id));

        if(oldNote is null)
            return Error.Validation("Notes.NoteId", "The Note with The Provide Id wasn't found");

        if(!string.IsNullOrEmpty(command.Description))
            oldNote.Description = command.Description;

        oldNote.IsPublic = command.IsPublic;

        oldNote.EditionDate = editionDate;

        _noteRepository.Update(oldNote);

        List<Note> notesToDelete = new List<Note>();

        if (command.NoteFilesIdsDelete is not null && command.NoteFilesIdsDelete.Count > 0)
        {
            foreach (Guid noteFileId in command.NoteFilesIdsDelete)
            {
                if (await _noteFileRepository.GetByIdAsync(new NoteFileId(noteFileId)) is NoteFile noteFile)
                {
                    _noteFileRepository.Delete(noteFile);
                }
            }
        }

        if (command.NotesList is not null && command.NotesList.Count > 0)
        {
            foreach (UpdateNotesObjectCommand item in command.NotesList)
            {
                NoteFile temp = new
                (
                    new NoteFileId(Guid.NewGuid()),
                    oldNote.Id,
                    item.FileName,
                    item.UrlFile,

                    true, // IsEditable
                    true, // IsDeleteable

                    creationDate,
                    editionDate
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

           oldNote.AssignedTo, //CollaboratorId

           creationDate,
           editionDate
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