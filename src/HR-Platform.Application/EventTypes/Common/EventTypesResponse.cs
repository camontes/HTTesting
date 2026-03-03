namespace HR_Platform.Application.EventTypes.Common;
public record EventTypesResponse
(
    Guid EventTypeId,
    string Name, 
    string NameEnglish
);
