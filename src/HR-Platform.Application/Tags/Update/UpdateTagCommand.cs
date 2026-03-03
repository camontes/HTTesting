using ErrorOr;
using HR_Platform.Application.Tags.Common;
using MediatR;

namespace HR_Platform.Application.Tags.Update;

public record UpdateTagCommand(
     string Id,

     string CompanyId,

    string Name,
    string NameEnglish
) : IRequest<ErrorOr<TagsResponse>>;

