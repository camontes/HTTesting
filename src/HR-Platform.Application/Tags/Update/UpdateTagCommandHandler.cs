using ErrorOr;
using HR_Platform.Application.Tags.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Tags;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HR_Platform.Application.Tags.Update;

internal sealed class UpdateCompanyCommandHandler(
    ITagRepository tagRepository,

    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateTagCommand, ErrorOr<TagsResponse>>
{
    private readonly ITagRepository _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<TagsResponse>> Handle(UpdateTagCommand command, CancellationToken cancellationToken)
    {
        /* Creation and edition dates */

        string editionDateString = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        /* Company validations */

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Tags.EditionDate", "EditionDate is not valid");

        /* Tag mapping */

        Tag tag = new
        (
            new(Guid.Parse(command.Id)),

            new(Guid.Parse(command.CompanyId)),

            command.Name,
            command.NameEnglish,

            true, // IsEditable
            true, // IsDeleteable

            editionDate, // creationDate,
            editionDate
        );

        _tagRepository.Update(tag);

        /* Save changes */

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        /* Return Company */

        TagsResponse tagResponse = new
        (
            tag.Id.Value,

            tag.CompanyId.Value,

            tag.Name,
            tag.NameEnglish,

            tag.IsEditable,
            tag.IsDeleteable,

            tag.CreationDate.Value,
            tag.EditionDate.Value
        );

        return tagResponse;
    }
}