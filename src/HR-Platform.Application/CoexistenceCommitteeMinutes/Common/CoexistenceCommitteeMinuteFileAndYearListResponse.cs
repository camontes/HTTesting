namespace HR_Platform.Application.CoexistenceCommitteeMinutes.Common;
public record CoexistenceCommitteeMinuteFileAndYearListResponse
(
    List<CoexistenceCommitteeMinuteFileResponse> CoexistenceCommitteeMinuteFilesList,
    List<string> Years
   
);

