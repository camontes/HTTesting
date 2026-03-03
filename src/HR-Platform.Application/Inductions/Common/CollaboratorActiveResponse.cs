namespace HR_Platform.Application.Inductions.Common;
public record CollaboratorActiveResponse
(
    string CollaboratorId,
    string Name,
    string PersonalEmail,
    string BusinessEmail,
    string PhotoURL,
    string ShortName,
    bool IsFinishedInduction
);

