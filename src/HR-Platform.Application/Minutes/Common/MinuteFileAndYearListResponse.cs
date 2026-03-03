namespace HR_Platform.Application.Minutes.Common;
public record MinuteFileAndYearListResponse
(
    List<MinuteFileResponse> MinuteFilesList,
    List<string> Years
   
);

