namespace HR_Platform.Application.BrigadeAdjustments.Common;

public record BrigadeAdjustmentsResponse(
    Guid Id,
    string Name,
    string NameEnglish,
    int IconId,
    bool IsEditable,
    bool IsDeleteable
);
