using ErrorOr;
using HR_Platform.Domain.CollaboratorTalentPools;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.TalentPools.RemoveCollaboratorTalentPool;

internal sealed class RemoveCollaboratorTalentPoolQueryHandler
(
    ICollaboratorTalentPoolRepository collaboratorTalentPoolRepository,

    IUnitOfWork unitOfWork
)
:
IRequestHandler<RemoveCollaboratorTalentPoolQuery, ErrorOr<bool>>
{
    private readonly ICollaboratorTalentPoolRepository _collaboratorTalentPoolRepository = collaboratorTalentPoolRepository ?? throw new ArgumentNullException(nameof(collaboratorTalentPoolRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(RemoveCollaboratorTalentPoolQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorTalentPoolRepository.GetByIdAsync(new CollaboratorTalentPoolId(query.CollaboratorTalentPoolId)) is not CollaboratorTalentPool oldCollaboratorTalentPool)
            return Error.NotFound("CollaboratorTalentPool.NotFound", "The Collaborator Talent Pool with the provide Id was not found.");

        try
        {
            _collaboratorTalentPoolRepository.Delete(oldCollaboratorTalentPool);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}