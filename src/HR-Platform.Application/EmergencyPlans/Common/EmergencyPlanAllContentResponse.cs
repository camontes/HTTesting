namespace HR_Platform.Application.EmergencyPlans.Common;
public record EmergencyPlanAllContentResponse
(
    string Id,
    string Description, 
    string ImageName,
    string ImageURL,
    string ImageCreationDate,
    string ImageCreationDateEnglish,
    string VideoName,
    string VideoURL,
    bool IsVisible
);

