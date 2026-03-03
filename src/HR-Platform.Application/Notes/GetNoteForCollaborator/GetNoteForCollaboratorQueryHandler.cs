using ErrorOr;
using HR_Platform.Application.Notes.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Notes;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Notes.GetNoteForCollaborator;

internal sealed class GetNoteForCollaboratorQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    INoteRepository noteRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetNoteForCollaboratorQuery, ErrorOr<List<NotesResponse>>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly INoteRepository _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<List<NotesResponse>>> Handle(GetNoteForCollaboratorQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByEmailAsync(query.CollaboratorEmail) is not Collaborator oldCollaborator)
            return Error.Validation("Notes.CollaboratorId", "The Collaborator with the provide Email wasn't found");

        List<Note> noteList = await _noteRepository.GetByCollaboratorIdAsync(oldCollaborator.Id);

        var filteredNotes = query.IsPublic
       ? noteList.Where(n => n.IsPublic).ToList() // Públicas
       : noteList.Where(n => !n.IsPublic && n.Assignee.BusinessEmail.Value == query.CollaboratorEmail).ToList(); // Privadas 

        var mainNotes = filteredNotes.Where(n => n.ParentNoteId == null).ToList();

        var response = mainNotes
            .Select(mainNote => MapNoteToResponse(mainNote, filteredNotes, query.CollaboratorEmail))
            .OrderByDescending(x => x.CreationDate)
            .ToList();

        return response;
    }
    private NotesResponse MapNoteToResponse(Note note, List<Note> allResponses, string email)
    {
        var filesList = note.NoteFiles.Select(file => new NoteFileResponse(
            file.Id.Value,
            file.FileName,
            file.UrlFile,
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", file.EditionDate.Value).Split('.')[0]),
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", file.EditionDate.Value).Split('.')[1]),
            file.EditionDate.Value,
            _timeFormatService.GetDateTimeFormatMonthToltip(file.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")),
            note.Creator.Photo,
            note.Creator.Name,
            _stringService.GetInitials(note.Creator.Name)
        )).ToList();

        var answers = allResponses
            .Where(response => response.ParentNoteId == note.Id)
            .OrderBy(r => r.CreationDate.Value)
            .Select(response => MapNoteToResponse(response, allResponses, email)) // Llamada recursiva
            .ToList();

        //David
        string textDateDifference = string.Empty;
        string textDateDifferenceEnglish = string.Empty;

        if (note.CreationDate.Value == note.EditionDate.Value)
        {
            textDateDifference = "Agregado";
            textDateDifferenceEnglish = "Added";
        }
        else
        {
            textDateDifference = "Actualizado";
            textDateDifferenceEnglish = "Updated";
        }
        //

        return new NotesResponse(
            note.Id.Value.ToString(),
            note.ParentNoteId?.Value.ToString(),
            note.Description,
            note.Creator.Name,
            _stringService.GetInitials(note.Creator.Name),
            note.Creator.Photo,
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction(textDateDifference, textDateDifferenceEnglish, note.EditionDate.Value).Split('.')[0]),
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction(textDateDifference, textDateDifferenceEnglish, note.EditionDate.Value).Split('.')[1]),
            _timeFormatService.GetDateFormatMonthLarge(note.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")),
            _timeFormatService.GetDateFormatMonthLarge(note.EditionDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")),
            _timeFormatService.GetDateTimeFormatMonthToltip(note.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")),
            note.CreationDate.Value,
            email == note.Creator.BusinessEmail.Value,  // IsEditable
            filesList,
            answers
        );
    }
}