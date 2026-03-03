namespace HR_Platform.Application.OrganizationCharts.Common;
public record OrganizationChartFileResponse
(
    Guid IdFile,
    bool IsByFile,
    bool IsByUrl,
    string FileName,
    string FileURL,
    string TimePosted,
    string TimePostedEnglish,
    string CreationDate,
    string CreationDateTooltip,
    string FullNameTh,
    string ShortNameTh
);

