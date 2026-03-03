namespace HR_Platform.Application.Risks.Common;
public record RiskResponse
(
    string Id,
    string Name,
    string Description, 
    string ImageName,
    string ImageURL,
    string ImageCreationDate,
    string ImageCreationDateEnglish,
    string VideoName,
    string VideoURL,
    bool IsVisible,
    DateTime EditionDate,
    DateTime CreatedDate
);

