namespace HR_Platform.Application.Areas.Common;
public record AreasResponse
(
    Guid AreaId,
    string Name, 
    string NameEnglish,
    bool IsFormsVisible
);
