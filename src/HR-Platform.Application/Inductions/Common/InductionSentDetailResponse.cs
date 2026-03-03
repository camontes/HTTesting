namespace HR_Platform.Application.Inductions.Common;
public record InductionSentDetailResponse
(
    string InductionName,
    string ShipmentDate,
    string ShipmentDateEnglish,
    string ShipmentDateToltip,
    DateTime ShipmentDateFormat,
    bool AllowForAllCollaborators
);

