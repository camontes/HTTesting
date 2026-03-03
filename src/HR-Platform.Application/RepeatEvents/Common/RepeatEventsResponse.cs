namespace HR_Platform.Application.RepeatEvents.Common;
public record RepeatEventsResponse
(
    List<RepeatEventObjectResponse> Months,
    List<RepeatEventObjectResponse> DaysOfWeek,
    List<RepeatEventObjectResponse> RepeatEvery,
    List<RepeatEventObjectResponse> RepeatType
);

public record RepeatEventObjectResponse
(
    int Id,
    string Name,
    string NameEnglish
);