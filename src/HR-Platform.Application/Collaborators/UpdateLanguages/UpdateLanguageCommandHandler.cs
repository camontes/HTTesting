using ErrorOr;
using HR_Platform.Application.Collaborators.UpdateLanguages;
using HR_Platform.Domain.CollaboratorLanguages;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DefaultLanguageLevels;
using HR_Platform.Domain.DefaultLanguageTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Assignations.Update;

internal sealed class UpdateLanguageCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorLanguageRepository collaboratorLanguageRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateLanguageCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorLanguageRepository _collaboratorLanguageRepository = collaboratorLanguageRepository ?? throw new ArgumentNullException(nameof(collaboratorLanguageRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateLanguageCommand command, CancellationToken cancellationToken)
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


        List<CollaboratorLanguage> collaboratorLanguagesList = await _collaboratorLanguageRepository.GetByCollaboratorIdAsync(oldCollaboratorId);
        List<CollaboratorLanguage> ListCreated = [];

        if (command.LanguageList.Count != 0)
        {
            IEnumerable<CollaboratorLanguageId> idsRegistrosExistentes = collaboratorLanguagesList.Select(obj => obj.Id);
            IEnumerable<CollaboratorLanguageId> idsRegistrosNuevos = command.LanguageList.Select(obj => new CollaboratorLanguageId(Guid.Parse(obj.Id))).Except(idsRegistrosExistentes);


            IEnumerable<CollaboratorLanguageId> idsRegistrosAEliminar = idsRegistrosExistentes.Except(command.LanguageList.Select(obj => new CollaboratorLanguageId(Guid.Parse(obj.Id))));

            if (idsRegistrosAEliminar.Any())
            {
                foreach (CollaboratorLanguageId DeleteId in idsRegistrosAEliminar)
                {
                    await _collaboratorLanguageRepository.DeleteById(DeleteId);
                }
            }

            List<UpdateLanguageRequest> newRegisters = command.LanguageList
                .Where(obj => idsRegistrosNuevos
                .Contains(new CollaboratorLanguageId(Guid.Parse(obj.Id))))
                .ToList();

            if (newRegisters.Count != 0)
            {
                foreach (UpdateLanguageRequest lg in newRegisters)
                {
                    CollaboratorLanguage newLanguage = new(
                        new CollaboratorLanguageId(Guid.NewGuid()),
                        oldCollaboratorId,
                        !string.IsNullOrEmpty(lg.LanguageLevelId) ? new DefaultLanguageLevelId(int.Parse(lg.LanguageLevelId)) : null,
                        !string.IsNullOrEmpty(lg.LanguageNameId) ? new DefaultLanguageTypeId(int.Parse(lg.LanguageNameId)) : null,
                        !string.IsNullOrEmpty(lg.OtherLanguageName) ? lg.OtherLanguageName : string.Empty,
                        !string.IsNullOrEmpty(lg.OtherLanguageName) ? lg.OtherLanguageName : string.Empty,
                        true,
                        true,
                        editionDate,
                        editionDate
                    );
                    ListCreated.Add(newLanguage);
                }
            }
            _collaboratorLanguageRepository.AddRange(ListCreated);

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
        else if (command.LanguageList.Count == 0 && collaboratorLanguagesList.Count != 0)
        {
            try
            {
                oldCollaborator.ChangedBy = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Role.Name : oldCollaborator.ChangedBy;
                oldCollaborator.EmailChangedBy = command.EmailChangeBy;
                oldCollaborator.EditionDate = editionDate;
                _collaboratorLanguageRepository.DeleteRange(collaboratorLanguagesList);
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