namespace HR_Platform.Application.Forms.Common;
public record FormsResponse
(
    Guid FormId,
    string Name,

    bool IsVisible,

    DateTime CreationTime
);

