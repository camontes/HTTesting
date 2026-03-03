namespace HR_Platform.Application.Events.Common;

public record FormatDateToCalendarResponse(
string Year,
string Month,
string Day,
string Hour,
string Minute
);
