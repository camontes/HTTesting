using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Tags.Create;

public record CreateTagsCommand(List<TagData> TagsDataList) : IRequest<ErrorOr<bool>>;

public record TagData(
    string CompanyId,
    string Name
);

