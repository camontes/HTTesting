using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Tags.Delete;

public record DeleteTagsCommand
(
    List<Guid> TagsList,
    Guid companyId
) : IRequest<ErrorOr<bool>>;

