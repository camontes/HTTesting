namespace HR_Platform.Application.BenefitClaimAnswers.Common;
public record ClaimSentResponse
(
    string CollaboratorId,
    string DocumentType,
    string OtherDocumentType,
    string Document,
    string Name,
    string EntranceDate,
    string EntranceDateEnglish,
    string Assignation,
    int CountToralClaims,
    List<ClaimSentHistoryResponse> DetailClaimsList
);

