using ErrorOr;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.CoexistenceCommitteeMembers.RemoveMember;

internal sealed class RemoveCoexistenceCommitteeMembersCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<RemoveCoexistenceCommitteeMemberCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(RemoveCoexistenceCommitteeMemberCommand command, CancellationToken cancellationToken)
    {

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(command.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The Collaborator with the provide Id was not found.");
        oldCollaborator.IsCoexistenceCommitteeMember = false;

        _collaboratorRepository.Update(oldCollaborator);

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