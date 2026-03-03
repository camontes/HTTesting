namespace HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.Common;
public record EvidenceCoexistenceCommitteeVoteFileAndYearListResponse
(
    List<EvidenceCoexistenceCommitteeVoteFileResponse> EvidenceCoexistenceCommitteeVoteFilesList,
    List<string> Years
   
);

