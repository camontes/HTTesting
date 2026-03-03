namespace HR_Platform.Application.Events.Common;

public record EventAttendeeResponse(
    Guid CollaboratorId,
    string Name,
    string ShortName,
    string Photo
);
