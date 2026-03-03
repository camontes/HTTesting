namespace HR_Platform.Application.Regulations.Common;
public record RegulationFileAndYearListResponse
(
    List<RegulationFileResponse> RegulationFilesList,
    List<string> Years
   
);

