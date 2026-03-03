using HR_Platform.Application.WorkplaceRecommendations.Common;

namespace HR_Platform.Application.ContractTypes.Common;

public record WorkplaceRecommendationsResponse(
    Guid CollaboratorId,
    string Document,
    string DocumentType,
    string Name,
    string IntranceDate,
    List<WorkplaceRecommendationFileResponse> WorkplaceRecommendationFile,
    List<string> FileYears
);
