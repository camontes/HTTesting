namespace HR_Platform.Application.Tags.Common;

public record TagsAndCountByCompanyResponse
(
    List<TagWIthCollaboratorCountResponse> Tags,
    int TagsCount
);
