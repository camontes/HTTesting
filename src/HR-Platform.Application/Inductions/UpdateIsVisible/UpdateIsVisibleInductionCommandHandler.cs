using ErrorOr;
using HR_Platform.Application.Inductions.UpdateIsVisible;
using HR_Platform.Domain.CollaboratorInductions;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace HR_Platform.Application.Inductions.UpdateIsVisibleInduction;

internal sealed class UpdateIsVisibleInductionCommandHandler(
    IInductionRepository inductionRepository,
    ICollaboratorInductionRepository collaboratorInductionRepository,
    ICollaboratorRepository collaboratorRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateIsVisibleInductionCommand, ErrorOr<bool>>
{
    private readonly IInductionRepository _inductionRepository = inductionRepository ?? throw new ArgumentNullException(nameof(inductionRepository));
    private readonly ICollaboratorInductionRepository _collaboratorInductionRepository = collaboratorInductionRepository ?? throw new ArgumentNullException(nameof(collaboratorInductionRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateIsVisibleInductionCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Induction.CreationDate", "CreationDate is not valid");

        if (await _inductionRepository.GetByIdAsync(new InductionId(command.InductionId)) is not Induction oldInduction)
            return Error.NotFound("Induction.NotFound", "The Induction with the provide Id was not found.");

        if (!command.IsVisible) // If It is not Visible change to False
        {
            oldInduction.IsVisible = false;
            List<CollaboratorInduction> inductionTempList = await _collaboratorInductionRepository.GetByInductionIdAsync(oldInduction.Id);
            _collaboratorInductionRepository.DeleteRange(inductionTempList);
        }
        else if (command.AllowForAllCollaborators) // If It's Allow For all, change to True and Visible True
        {
            oldInduction.IsVisible = true;
            oldInduction.AllowForAllCollaborators = true;
            oldInduction.IsVisibleChangeDate = creationDate;
            List<CollaboratorInduction> inductionTempList = await _collaboratorInductionRepository.GetByInductionIdAsync(oldInduction.Id);
            _collaboratorInductionRepository.DeleteRange(inductionTempList);
        }
        else if (!command.AllowForAllCollaborators) // If It is not Allow For all Change to True and Visible True -  Green Eye
        {
            oldInduction.IsVisible = true;
            oldInduction.AllowForAllCollaborators = false;
            oldInduction.IsVisibleChangeDate = creationDate;

            if (command.CollaboratorIds is null || command.CollaboratorIds.Count == 0)
                return Error.Validation("Induction.CollaboratorId", "Collaborator Id list is required");

            if (command.CollaboratorIds.Count > 20)
                return Error.Validation("Induction.CollaboratorList", "Only 20 Collaborators maximum allowed");

            List<CollaboratorInduction> collaboratorInductionList = [];
            foreach (Guid item in command.CollaboratorIds)
            {
                if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(item)) is not Collaborator oldCollaborator)
                    return Error.Validation("Induction.CollaboratorId", "The Collaborator with the provide Id Was not found");

                var IsExistCollaborator = await _collaboratorInductionRepository.GetByCollaboratorAndInductionIdAsync(oldCollaborator.Id, oldInduction.Id);
                if (IsExistCollaborator.Count == 0)
                {
                    CollaboratorInduction temp = new
                    (
                        new CollaboratorInductionId(Guid.NewGuid()),
                        oldCollaborator.Id,
                        new InductionId(command.InductionId),
                        true,
                        true,
                        creationDate,
                        creationDate
                    );
                    collaboratorInductionList.Add(temp);
                }
            }
            if (collaboratorInductionList.Count > 0)
            {
                _collaboratorInductionRepository.AddRange(collaboratorInductionList);
            }
        }

        _inductionRepository.Update(oldInduction);

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