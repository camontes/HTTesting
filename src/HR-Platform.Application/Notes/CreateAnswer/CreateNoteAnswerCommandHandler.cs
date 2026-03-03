using ErrorOr;
using HR_Platform.Domain.CollaboratorBenefitClaims;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.NoteFiles;
using HR_Platform.Domain.Notes;
using HR_Platform.Domain.NotificationNotes;
using HR_Platform.Domain.Notifications;
using HR_Platform.Domain.NotificationTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Notes.CreateAnswer;

internal sealed class CreateNoteAnswerCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    INoteRepository noteRepository,
    INotificationNoteRepository notificationNoteRepository,
    INoteFileRepository noteFileRepository,
    INotificationRepository notificationRepository,
    INotificationTypeRepository notificationTypeRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateNoteAnswerCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly INoteRepository _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
    private readonly INotificationNoteRepository _notificationNoteRepository = notificationNoteRepository ?? throw new ArgumentNullException(nameof(notificationNoteRepository));
    private readonly INoteFileRepository _noteFileRepository = noteFileRepository ?? throw new ArgumentNullException(nameof(noteFileRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly INotificationTypeRepository _notificationTypeRepository = notificationTypeRepository ?? throw new ArgumentNullException(nameof(notificationTypeRepository));
    private readonly INotificationRepository _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));

    public async Task<ErrorOr<bool>> Handle(CreateNoteAnswerCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Notes.CreationDate", "CreationDate is not valid");

        if (await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy) is not Collaborator collaboratorWhoCreated)
            return Error.Validation("Notes.CollaboratorEmail", "The Collaborator WHO CREATED with The Provide Id wasn't found");

        if (await _noteRepository.GetByIdAsync(new NoteId(command.MainNoteId)) is not Note oldNote)
            return Error.Validation("Notes.MainNoteId", "The Main Note with the provide Id wasn't found");

        if (collaboratorWhoCreated is null)
            return Error.Validation("Notes.CollaboratorId", "The Collaborator WHO CREATED with The Provide Id wasn't found");

        List<NoteFile> noteFiles = [];

        Note noteResult = new
        (
            new NoteId(Guid.NewGuid()),
            command.Description,
            collaboratorWhoCreated.Id, //CreatedBy
            oldNote.AssignedTo, //AssignedTo
            new NoteId(command.MainNoteId), // ParentNoteId
            oldNote.IsPublic,
            true, //IsEditable
            true, //IsDeleletable
            creationDate,
            creationDate
        );

        _noteRepository.Add(noteResult);

        if (command.NotesList is not null && command.NotesList.Count > 0)
        {
            foreach (NoteAnswersFileObject item in command.NotesList)
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

        if (command.EmailChangeBy == oldNote.Creator.BusinessEmail.Value)
        {

            NotificationNote notification = new
            (
               new NotificationNoteId(Guid.NewGuid()),
               false, //IsRead 
               true, //IsEditable
               true, //IsDeletable
               oldNote.AssignedTo, //CollaboratorId
               creationDate,
               creationDate
            );
            _notificationNoteRepository.Add(notification);
        }
        else
        {
            NotificationType? notificationType = await _notificationTypeRepository.GetByIdAsync(new NotificationTypeId(5));

            if (notificationType is null)
                return Error.Validation("Notes.NotificationType", "The Notification Type with The Provide Id wasn't found");


            Notification notificationBell = new
            (
            new NotificationId(Guid.NewGuid()),

                notificationType.Message.Replace("@1", "<em>" + collaboratorWhoCreated.Name + "</em>"),
                notificationType.MessageEnglish.Replace("@1", "<em>" + collaboratorWhoCreated.Name + "</em>"),

                "", // SourceEmail
                collaboratorWhoCreated.Id.Value.ToString(), // SourceName - Use this field to save the Id of collaborator who is sending the answer
                "", // SourceInitials

                "https://hr-platform.s3.us-east-1.amazonaws.com/DefaultIconsDev/Bell.png",

                false, // IsRead

                true, // IsEditable
                true, // IsDeleteable

                oldNote.CreatedBy,
                notificationType.Id,

                creationDate,
                creationDate
             );
            _notificationRepository.Add(notificationBell);
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