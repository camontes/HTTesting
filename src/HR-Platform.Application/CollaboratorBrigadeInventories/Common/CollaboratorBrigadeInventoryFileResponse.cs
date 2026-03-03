namespace HR_Platform.Application.CollaboratorBrigadeInventories.Common;
public record CollaboratorBrigadeInventoryFileResponse
(
    Guid IdFile,
    string FileName,
    string FileURL,
    string CreationDateFile,
    string CreationDateFileEnglish,
    bool IsDeleted
);

