using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Tags;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Tags.Delete;

internal sealed class DeleteTagsCommandHandler
(
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteTagsCommand, ErrorOr<bool>>
{
    private readonly ITagRepository _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteTagsCommand query, CancellationToken cancellationToken)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        /* Match Tags */

        List<Tag>? tags = await _tagRepository.GetByCompanyIdAsync(new CompanyId(query.companyId), 0, 0);

        List<Tag>? tagsMatched = tags?.Where(x => query.TagsList.Any(y => new TagId(y) == x.Id)).ToList();
        
        List<Tag>? tagsNotMatched = [];
        if (tagsMatched != null && tagsMatched.Count > 0)
            tagsNotMatched = tags?.Except(tagsMatched).ToList();

        /* Only 1 Tag validation */

        if (tagsNotMatched == null || tagsNotMatched.Count == 0)
            return Error.NotFound("Tags.Count", "The Tags cannot be deleted");

        try
        {
            if(tagsMatched != null && tagsMatched.Count > 0)
            {
                _tagRepository.DeleteRange(tagsMatched);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}