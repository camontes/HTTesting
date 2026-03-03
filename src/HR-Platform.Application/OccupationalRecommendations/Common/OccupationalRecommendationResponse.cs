using HR_Platform.Application.OccupationalRecommendations.Common;

namespace HR_Platform.Application.ContractTypes.Common;

public record OccupationalRecommendationsResponse(
    Guid CollaboratorId,
    string Document,
    string DocumentType,
    string Name,
    string IntranceDate,
    List<OccupationalRecommendationFileResponse> OccupationalRecommendationFile,
    List<string> FileYears
);
