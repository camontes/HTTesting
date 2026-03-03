using ErrorOr;
using HR_Platform.Application.Collaborators.UpdateSoftSkills;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.CollaboratorSoftSkills;
using HR_Platform.Domain.DefaultSoftSkills;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Assignations.Update;

internal sealed class UpdateSoftSkillCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorSoftSkillRepository collaboratorSoftSkillRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateSoftSkillCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorSoftSkillRepository _collaboratorSoftSkillRepository = collaboratorSoftSkillRepository ?? throw new ArgumentNullException(nameof(collaboratorSoftSkillRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateSoftSkillCommand command, CancellationToken cancellationToken)
    {

        TimeZoneInfo colombiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, colombiaTimeZone);

        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        CollaboratorId oldCollaboratorId = new(command.CollaboratorId);

        if (await _collaboratorRepository.GetByIdAsync(oldCollaboratorId) is not Collaborator oldCollaborator)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);


        List<CollaboratorSoftSkill> collaboratorSoftSkillsList = await _collaboratorSoftSkillRepository.GetByCollaboratorIdAsync(oldCollaboratorId);
        List<CollaboratorSoftSkill> ListCreated = [];

        if (command.SoftSkillList.Count != 0)
        {
            IEnumerable<CollaboratorSoftSkillId> idsRegistrosExistentes = collaboratorSoftSkillsList.Select(obj => obj.Id);
            IEnumerable<CollaboratorSoftSkillId> idsRegistrosNuevos = command.SoftSkillList.Select(obj => new CollaboratorSoftSkillId(Guid.Parse(obj.Id))).Except(idsRegistrosExistentes);


            IEnumerable<CollaboratorSoftSkillId> idsRegistrosAEliminar = idsRegistrosExistentes.Except(command.SoftSkillList.Select(obj => new CollaboratorSoftSkillId(Guid.Parse(obj.Id))));

            if (idsRegistrosAEliminar.Any())
            {
                foreach (CollaboratorSoftSkillId DeleteId in idsRegistrosAEliminar)
                {
                    await _collaboratorSoftSkillRepository.DeleteById(DeleteId);
                }
            }

            List<UpdateSoftSkillRequest> newRegisters = command.SoftSkillList
                .Where(obj => idsRegistrosNuevos
                .Contains(new CollaboratorSoftSkillId(Guid.Parse(obj.Id))))
                .ToList();

            if (newRegisters.Count != 0)
            {
                foreach (UpdateSoftSkillRequest lg in newRegisters)
                {
                    CollaboratorSoftSkill? newSoftSkill = new(
                        new CollaboratorSoftSkillId(Guid.NewGuid()),
                        oldCollaboratorId,
                        !string.IsNullOrEmpty(lg.SoftSkillNameId) ? new DefaultSoftSkillId(int.Parse(lg.SoftSkillNameId)) : null,
                        !string.IsNullOrEmpty(lg.OtherSoftSkillName) ? lg.OtherSoftSkillName : string.Empty,
                        !string.IsNullOrEmpty(lg.OtherSoftSkillName) ? lg.OtherSoftSkillName : string.Empty,
                        true,
                        true,
                        editionDate,
                        editionDate
                    );
                    ListCreated.Add(newSoftSkill);
                }
            }
            _collaboratorSoftSkillRepository.AddRange(ListCreated);

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
        else if(command.SoftSkillList.Count == 0 && collaboratorSoftSkillsList.Count != 0)
        {
            try
            {
                oldCollaborator.ChangedBy = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Role.Name : oldCollaborator.ChangedBy;
                oldCollaborator.EmailChangedBy = command.EmailChangeBy;
                oldCollaborator.EditionDate = editionDate;
                _collaboratorSoftSkillRepository.DeleteRange(collaboratorSoftSkillsList);
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