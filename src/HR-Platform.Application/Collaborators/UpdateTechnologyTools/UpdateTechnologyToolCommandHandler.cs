using ErrorOr;
using HR_Platform.Application.Collaborators.UpdateTechnologyTools;
using HR_Platform.Domain.CollaboratorTechnologyTools;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DefaultKnowledgeLevels;
using HR_Platform.Domain.DefaultTechnologyNames;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Assignations.Update;

internal sealed class UpdateTechnologyToolCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorTechnologyToolRepository collaboratorTechnologyToolRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateTechnologyToolCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorTechnologyToolRepository _collaboratorTechnologyToolRepository = collaboratorTechnologyToolRepository ?? throw new ArgumentNullException(nameof(collaboratorTechnologyToolRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateTechnologyToolCommand command, CancellationToken cancellationToken)
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

        List<CollaboratorTechnologyTool> collaboratorTechnologyToolsList = await _collaboratorTechnologyToolRepository.GetByCollaboratorIdAsync(oldCollaboratorId);
        List<CollaboratorTechnologyTool> ListCreated = [];

        if (command.TechnologyToolList.Count != 0)
        {
            IEnumerable<CollaboratorTechnologyToolId> idsRegistrosExistentes = collaboratorTechnologyToolsList.Select(obj => obj.Id);
            IEnumerable<CollaboratorTechnologyToolId> idsRegistrosNuevos = command.TechnologyToolList.Select(obj => new CollaboratorTechnologyToolId(Guid.Parse(obj.Id))).Except(idsRegistrosExistentes);


            IEnumerable<CollaboratorTechnologyToolId> idsRegistrosAEliminar = idsRegistrosExistentes.Except(command.TechnologyToolList.Select(obj => new CollaboratorTechnologyToolId(Guid.Parse(obj.Id))));

            if (idsRegistrosAEliminar.Any())
            {
                foreach (CollaboratorTechnologyToolId DeleteId in idsRegistrosAEliminar)
                {
                    await _collaboratorTechnologyToolRepository.DeleteById(DeleteId);
                }
            }

            List<UpdateTechnologyToolRequest> newRegisters = command.TechnologyToolList
                .Where(obj => idsRegistrosNuevos
                .Contains(new CollaboratorTechnologyToolId(Guid.Parse(obj.Id))))
                .ToList();

            if (newRegisters.Count != 0)
            {
                foreach (UpdateTechnologyToolRequest lg in newRegisters)
                {
                    CollaboratorTechnologyTool? newTechnologyTool = new(
                        new CollaboratorTechnologyToolId(Guid.NewGuid()),
                        oldCollaboratorId,
                        !string.IsNullOrEmpty(lg.TechnologyToolNameId) ? new DefaultTechnologyNameId(int.Parse(lg.TechnologyToolNameId)) : null,
                        !string.IsNullOrEmpty(lg.OtherKnowledgeLevelId) ? new DefaultKnowledgeLevelId(int.Parse(lg.OtherKnowledgeLevelId)) : null,
                        !string.IsNullOrEmpty(lg.OtherTechnologyToolName) ? lg.OtherTechnologyToolName : string.Empty,
                        !string.IsNullOrEmpty(lg.OtherTechnologyToolName) ? lg.OtherTechnologyToolName : string.Empty,
                        !string.IsNullOrEmpty(lg.OtherKnowledgeLevelName) ? lg.OtherKnowledgeLevelName : string.Empty,
                        !string.IsNullOrEmpty(lg.OtherKnowledgeLevelName) ? lg.OtherKnowledgeLevelName : string.Empty,
                        true,
                        true,
                        editionDate,
                        editionDate
                    );
                    ListCreated.Add(newTechnologyTool);
                }
            }
            _collaboratorTechnologyToolRepository.AddRange(ListCreated);

            try
            {
                oldCollaborator.ChangedBy = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Role.Name : oldCollaborator.ChangedBy;
                oldCollaborator.EmailChangedBy = command.EmailChangeBy;
                oldCollaborator.EditionDate = editionDate;
                _collaboratorRepository.Update( oldCollaborator );  

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }else if (command.TechnologyToolList.Count == 0 && collaboratorTechnologyToolsList.Count != 0)
        {
            try
            {
                oldCollaborator.ChangedBy = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Role.Name : oldCollaborator.ChangedBy;
                oldCollaborator.EmailChangedBy = command.EmailChangeBy;
                oldCollaborator.EditionDate = editionDate;
                _collaboratorTechnologyToolRepository.DeleteRange(collaboratorTechnologyToolsList);
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