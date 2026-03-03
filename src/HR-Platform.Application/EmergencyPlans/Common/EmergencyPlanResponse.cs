namespace HR_Platform.Application.EmergencyPlans.Common;
public record EmergencyPlanResponse
(
    List<EmergencyPlanObjectRiskResponse> RisksList,
    List<EmergencyPlanObjectResponse> MeetingEvacuationPointList,
    List<EmergencyPlanObjectResponse> InCaseOfEmergencyList,
    List<EmergencyPlanObjectResponse> ActivitiesList
);

public record EmergencyPlanObjectRiskResponse
(
    string Id,
    string Name,
    string NameEnglish,
    bool IsVisible,
    DateTime CreationTime,
    int Indicator
);

public record EmergencyPlanObjectResponse
(
    string Id,
    string Name,
    string NameEnglish,
    bool IsVisible,
    DateTime CreationTime
);

