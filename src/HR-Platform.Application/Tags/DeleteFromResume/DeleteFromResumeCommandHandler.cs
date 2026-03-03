using ErrorOr;
using HR_Platform.Application.Tags.DeleteFromResume;
using HR_Platform.Domain.CollaboratorTags;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.Tags.GetByCollaborator;

internal sealed class DeleteFromResumeCommandHandler(
    ICollaboratorTagRepository collaboratorTagRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteFromResumeCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorTagRepository _collaboratorTagRepository = collaboratorTagRepository ?? throw new ArgumentNullException(nameof(collaboratorTagRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteFromResumeCommand query, CancellationToken cancellationToken)
    {
        if (await _collaboratorTagRepository.GetByIdAsync(new CollaboratorTagId(query.CollaboratorTagId)) is not CollaboratorTag oldCollaboratorTag)
            return Error.NotFound("Tag.NotFound", "The Collaborator Tag with the provide Id was not found.");

        _collaboratorTagRepository.Delete(oldCollaboratorTag);

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}