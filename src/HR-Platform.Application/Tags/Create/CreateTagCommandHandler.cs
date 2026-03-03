using ErrorOr;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Tags;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Tags.Create;

internal sealed class CreateTagsCommandHandler(ITagRepository TagRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateTagsCommand, ErrorOr<bool>>
{
    private readonly ITagRepository _TagRepository = TagRepository ?? throw new ArgumentNullException(nameof(TagRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateTagsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Tags.CreationDate", "CreationDate is not valid");

        List<Tag> tagsToAdd = [];

        foreach (TagData tagData in command.TagsDataList)
        {
            Tag tag = new(
                new TagId(Guid.NewGuid()),
                new CompanyId(Guid.Parse(tagData.CompanyId)),
                tagData.Name,
                tagData.Name, //NameEnglish,
                true, //IsEditable,
                true, //IsDeleteable,
                creationDate,
                creationDate //EditionDate
            );
            tagsToAdd.Add(tag);
        }

        try
        {
            _TagRepository.AddRangeTags(tagsToAdd);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}