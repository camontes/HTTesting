using ErrorOr;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.CollaboratorTags;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.Tags;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Tags.AddCollaborator;

internal sealed class AddCollaboratorTagCommandHandler(
    ITagRepository tagRepository,
    ICollaboratorTagRepository collaboratorTagRepository,
    ICollaboratorRepository collaboratorlRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddCollaboratorTagCommand, ErrorOr<bool>>
{
    private readonly ITagRepository _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
    private readonly ICollaboratorTagRepository _collaboratorTagRepository = collaboratorTagRepository ?? throw new ArgumentNullException(nameof(collaboratorTagRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorlRepository ?? throw new ArgumentNullException(nameof(collaboratorlRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(AddCollaboratorTagCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Tags.CreationDate", "CreationDate is not valid");

        if (await _tagRepository.GetByIdAsync(new TagId(command.TagId)) is not Tag oldTag)
            return Error.NotFound("Tag.NotFound", "The Tag with the provide Id was not found.");

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(command.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.NotFound("Tag.NotFound", "The Collaborator with the provide Id was not found.");


        bool isExistCollaboratorInTag = await _collaboratorTagRepository.IsExistCollaboratorAsync(oldTag.Id, oldCollaborator.Id);

        if (isExistCollaboratorInTag)
            return Error.Validation("Tag.ExistTag", "The Tag has already been assigned to the resume.");

        CollaboratorTag collaboratorTag = new
        (
            new CollaboratorTagId(Guid.NewGuid()),
            oldCollaborator.Id,
            oldTag.Id,
            creationDate,
            creationDate
        );

        try
        {
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