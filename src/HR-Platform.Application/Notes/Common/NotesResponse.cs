namespace HR_Platform.Application.Notes.Common;
public record NotesResponse
(
    string NoteId,
    string? ParentNoteId,
    string Description,
    string FullNameWhoCreated,
    string ShortNameWhoCreated,
    string PhotoUrlWhoCreated,
    string TimeCreatedFormatText,
    string TimeCreatedEnglishText,
    string TimeCreatedFormat,
    string TimeCreatedEnglish,
    string TimeCreatedToltip,
    DateTime CreationDate,
    bool IsEditable,
    List<NoteFileResponse> NoteFilesList,
    List<NotesResponse> Answers
);

