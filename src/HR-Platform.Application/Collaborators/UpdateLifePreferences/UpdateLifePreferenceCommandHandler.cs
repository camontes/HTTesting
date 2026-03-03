using ErrorOr;
using HR_Platform.Application.Collaborators.UpdateLifePreferences;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.CollaboratorLifePreferences;
using HR_Platform.Domain.DefaultLifePreferences;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Assignations.Update;

internal sealed class UpdateLifePreferenceCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorLifePreferenceRepository collaboratorLifePreferenceRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateLifePreferenceCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorLifePreferenceRepository _collaboratorLifePreferenceRepository = collaboratorLifePreferenceRepository ?? throw new ArgumentNullException(nameof(collaboratorLifePreferenceRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateLifePreferenceCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");
        CollaboratorId oldCollaboratorId = new(command.CollaboratorId);

        if (await _collaboratorRepository.GetByIdAsync(oldCollaboratorId) is not Collaborator oldCollaborator)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<CollaboratorLifePreference> collaboratorLifePreferencesList = await _collaboratorLifePreferenceRepository.GetByCollaboratorIdAsync(oldCollaboratorId);
        List<CollaboratorLifePreference> ListCreated = [];

        if (command.LifePreferenceList.Count != 0)
        {
            IEnumerable<CollaboratorLifePreferenceId> idsRegistrosExistentes = collaboratorLifePreferencesList.Select(obj => obj.Id);
            IEnumerable<CollaboratorLifePreferenceId> idsRegistrosNuevos = command.LifePreferenceList.Select(obj => new CollaboratorLifePreferenceId(Guid.Parse(obj.Id))).Except(idsRegistrosExistentes);


            IEnumerable<CollaboratorLifePreferenceId> idsRegistrosAEliminar = idsRegistrosExistentes.Except(command.LifePreferenceList.Select(obj => new CollaboratorLifePreferenceId(Guid.Parse(obj.Id))));

            if (idsRegistrosAEliminar.Any())
            {
                foreach (CollaboratorLifePreferenceId DeleteId in idsRegistrosAEliminar)
                {
                    await _collaboratorLifePreferenceRepository.DeleteById(DeleteId);
                }
            }

            List<UpdateLifePreferenceRequest> newRegisters = command.LifePreferenceList
                .Where(obj => idsRegistrosNuevos
                .Contains(new CollaboratorLifePreferenceId(Guid.Parse(obj.Id))))
                .ToList();

            if (newRegisters.Count != 0)
            {
                foreach (UpdateLifePreferenceRequest lg in newRegisters)
                {
                    CollaboratorLifePreference? newLifePreference = new(
                        new CollaboratorLifePreferenceId(Guid.NewGuid()),
                        oldCollaboratorId,
                        !string.IsNullOrEmpty(lg.LifePreferenceNameId) ? new DefaultLifePreferenceId(int.Parse(lg.LifePreferenceNameId)) : null,
                        !string.IsNullOrEmpty(lg.OtherLifePreferenceName) ? lg.OtherLifePreferenceName : string.Empty,
                        !string.IsNullOrEmpty(lg.OtherLifePreferenceName) ? lg.OtherLifePreferenceName : string.Empty,
                        true,
                        true,
                        editionDate,
                        editionDate
                    );
                    ListCreated.Add(newLifePreference);
                }
            }
            _collaboratorLifePreferenceRepository.AddRange(ListCreated);

            try
            {
                oldCollaborator.ChangedBy = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Role.Name : oldCollaborator.ChangedBy;
                oldCollaborator.EmailChangedBy = command.EmailChangeBy;
                oldCollaborator.EditionDate = editionDate;
                _collaboratorRepository.Update(oldCollaborator);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else if (command.LifePreferenceList.Count == 0 && collaboratorLifePreferencesList.Count != 0)
        {
            try
            {
                oldCollaborator.ChangedBy = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Role.Name : oldCollaborator.ChangedBy;
                oldCollaborator.EmailChangedBy = command.EmailChangeBy;
                oldCollaborator.EditionDate = editionDate;
                _collaboratorLifePreferenceRepository.DeleteRange(collaboratorLifePreferencesList);
                _collaboratorRepository.Update(oldCollaborator);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        return true;
    }
}