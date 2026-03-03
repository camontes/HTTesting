using ErrorOr;
using HR_Platform.Application.Tags.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Tags;
using MediatR;

namespace HR_Platform.Application.Tags.GetByCompanyId;

internal sealed class GetTagsByCompanyIdHandler(
    ITagRepository tagRepository
    ) : IRequestHandler<GetTagsByCompanyIdQuery, ErrorOr<TagsAndCountByCompanyResponse>>
{
    private readonly ITagRepository _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));

    public async Task<ErrorOr<TagsAndCountByCompanyResponse>> Handle(GetTagsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _tagRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), query.Page, query.PageSize) is not List<Tag> tags)
            return Error.NotFound("Tags.NotFound", "The tags related with the provide Company Id was not found.");

        List<TagWIthCollaboratorCountResponse> tagsResponse = [];

        if (tags is not null && tags.Count > 0)
        {
            foreach (Tag tag in tags)
            {
                tagsResponse.Add
                (
                    new TagWIthCollaboratorCountResponse
                    (
                        tag.Id.Value,
                        tag.CompanyId.Value,

                        tag.Name,
                        tag.NameEnglish,

                        tag.CollaboratorTags.Count,

                        tag.IsEditable,
                        tag.IsDeleteable,

                        tag.CreationDate.Value,
                        tag.EditionDate.Value
                    )
                );
            }
        }

        TagsAndCountByCompanyResponse finalResult = new(
            tagsResponse,
            tagsResponse.Count
        );

        return finalResult;

    }
}