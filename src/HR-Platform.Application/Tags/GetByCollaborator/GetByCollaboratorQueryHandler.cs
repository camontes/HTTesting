using ErrorOr;
using HR_Platform.Application.Tags.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.CollaboratorTags;
using MediatR;

namespace HR_Platform.Application.Tags.GetByCollaborator;

internal sealed class GetByCollaboratorQueryHandler(
    ICollaboratorTagRepository collaboratorTagRepository,
    ICollaboratorRepository collaboratorlRepository
    ) : IRequestHandler<GetByCollaboratorQuery, ErrorOr<List<TagNamesResponse>>>
{
    private readonly ICollaboratorTagRepository _collaboratorTagRepository = collaboratorTagRepository ?? throw new ArgumentNullException(nameof(collaboratorTagRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorlRepository ?? throw new ArgumentNullException(nameof(collaboratorlRepository));

    public async Task<ErrorOr<List<TagNamesResponse>>> Handle(GetByCollaboratorQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(query.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.NotFound("Tag.NotFound", "The Collaborator with the provide Id was not found.");

        List<CollaboratorTag> collaboratorTags = await _collaboratorTagRepository.GetByCollaboratorIdAsync(oldCollaborator.Id);
        List<TagNamesResponse> tagNames = [];

        if (collaboratorTags is not null && collaboratorTags.Count > 0)
        {
            foreach (CollaboratorTag item in collaboratorTags)
            {
                TagNamesResponse temp = new
                (
                    item.Id.Value,
                    item.Tag.Name,
                    item.Tag.NameEnglish
                );
                tagNames.Add(temp);
            }
        }

        return tagNames;
    }
}