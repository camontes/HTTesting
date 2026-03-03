namespace HR_Platform.Application.DocumentManagements.Common;

public record DocumentManagementFileFormatResponse
(
    string FileName,
    string FileURL,
    int FileTypeId,
    string Others
);
