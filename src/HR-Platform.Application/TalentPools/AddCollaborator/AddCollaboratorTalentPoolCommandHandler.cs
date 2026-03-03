using ErrorOr;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.CollaboratorTalentPools;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.TalentPools;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.TalentPools.AddCollaborator;

internal sealed class AddCollaboratorTalentPoolCommandHandler(
    ITalentPoolRepository talentPoolRepository,
    ICollaboratorTalentPoolRepository collaboratorTalentPoolRepository,
    ICollaboratorRepository collaboratorlRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddCollaboratorTalentPoolCommand, ErrorOr<bool>>
{
    private readonly ITalentPoolRepository _talentPoolRepository = talentPoolRepository ?? throw new ArgumentNullException(nameof(talentPoolRepository));
    private readonly ICollaboratorTalentPoolRepository _collaboratorTalentPoolRepository = collaboratorTalentPoolRepository ?? throw new ArgumentNullException(nameof(collaboratorTalentPoolRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorlRepository ?? throw new ArgumentNullException(nameof(collaboratorlRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(AddCollaboratorTalentPoolCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("TalentPools.CreationDate", "CreationDate is not valid");

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(command.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.NotFound("TalentPool.NotFound", "The Collaborator with the provide Id was not found.");

        List<CollaboratorTalentPool> pools = [];

        foreach (Guid item in command.TalentPoolIds)
        {
            TalentPoolId talentId = new(item);
            bool isExistCollaboratorInTalentPool = await _collaboratorTalentPoolRepository.IsExistCollaboratorAsync(talentId, oldCollaborator.Id);

            if (isExistCollaboratorInTalentPool)
                return Error.Validation("TalentPool.CollaboratorInTalentPool", "The collaborator is already in the talent pool.");

            CollaboratorTalentPool collaboratorTalentPool = new
            (
                new CollaboratorTalentPoolId(Guid.NewGuid()),
                oldCollaborator.Id,
                talentId,
                creationDate,
                creationDate
            );
            pools.Add(collaboratorTalentPool);
        }

        try
        {
            _collaboratorTalentPoolRepository.AddRange(pools);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}