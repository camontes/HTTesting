using ErrorOr;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.CollaboratorTags;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Tags;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Tags.CreateFromResume;

internal sealed class CreateFromResumeTagsCommandHandler(
    ITagRepository TagRepository,
    ICollaboratorRepository collaboratorlRepository,
    ICollaboratorTagRepository collaboratorTagRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateFromResumeTagsCommand, ErrorOr<bool>>
{
    private readonly ITagRepository _TagRepository = TagRepository ?? throw new ArgumentNullException(nameof(TagRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorlRepository ?? throw new ArgumentNullException(nameof(collaboratorlRepository));
    private readonly ICollaboratorTagRepository _collaboratorTagRepository = collaboratorTagRepository ?? throw new ArgumentNullException(nameof(collaboratorTagRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateFromResumeTagsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Tags.CreationDate", "CreationDate is not valid");

        List<Tag> tagsToAdd = [];

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(command.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.NotFound("Tag.NotFound", "The Collaborator with the provide Id was not found.");

        Tag tag = new(
            new TagId(Guid.NewGuid()),
            new CompanyId(command.CompanyId),
            command.Name,
            command.Name, //NameEnglish,
            true, //IsEditable,
            true, //IsDeleteable,
            creationDate,
        creationDate //EditionDate
        );

        CollaboratorTag collaboratorTag = new
        (
            new CollaboratorTagId(Guid.NewGuid()),
            oldCollaborator.Id,
            tag.Id,
            creationDate,
            creationDate
        );

        try
        {
            _TagRepository.Add(tag);
            _collaboratorTagRepository.Add(collaboratorTag);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}