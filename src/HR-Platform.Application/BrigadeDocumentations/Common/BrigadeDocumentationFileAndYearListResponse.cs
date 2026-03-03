namespace HR_Platform.Application.BrigadeDocumentations.Common;
public record BrigadeDocumentationFileAndYearListResponse
(
    List<BrigadeDocumentationFileResponse> BrigadeDocumentationFilesList,
    List<string> Years
   
);

