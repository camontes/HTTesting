using ErrorOr;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.CopasstMembers.Create;

internal sealed class CreateCopasstMembersCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateCopasstMembersCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateCopasstMembersCommand command, CancellationToken cancellationToken)
    {
        List<Collaborator> allCollaborators = await _collaboratorRepository.GetAllCopasst();

        // Filtra los colaboradores que coinciden con los IDs

        if (command.CollaboratorIds is not null && command.CollaboratorIds.Count > 0)
        {
            List<Collaborator> tempList = [];
            List<Collaborator> collaboratorsToUpdate = allCollaborators
                .Where(c => command.CollaboratorIds.Contains(c.Id.Value))
                .ToList();

            foreach (Collaborator collaborator in collaboratorsToUpdate)
            {
                collaborator.IsCopasstMember = true;
                tempList.Add(collaborator);
            }
            if (tempList.Count > 0)
            {
                _collaboratorRepository.UpdateRange(tempList);
            }
        }

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