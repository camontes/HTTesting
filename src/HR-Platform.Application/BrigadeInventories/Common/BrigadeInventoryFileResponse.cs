namespace HR_Platform.Application.BrigadeInventories.Common;
public record BrigadeInventoryFileResponse
(
    Guid IdFile,
    string FileName,
    string FileURL,
    string CreationDateFile,
    string CreationDateFileEnglish
);

