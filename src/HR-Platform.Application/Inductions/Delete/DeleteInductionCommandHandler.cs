using ErrorOr;
using HR_Platform.Domain.CollaboratorGeneralInductions;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Inductions.Delete;

internal sealed class GetInductionByCompanyIdCommandHandler(
    IInductionRepository inductionRepository,
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorGeneralInductionRepository collaboratorGeneralInductionRepository,
    IUnitOfWork unitOfWork

    ) : IRequestHandler<DeleteInductionCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IInductionRepository _inductionRepository = inductionRepository ?? throw new ArgumentNullException(nameof(inductionRepository));
    private readonly ICollaboratorGeneralInductionRepository _collaboratorGeneralInductionRepository = collaboratorGeneralInductionRepository ?? throw new ArgumentNullException(nameof(collaboratorGeneralInductionRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteInductionCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string deleteDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(deleteDateString) is not TimeDate deleteDate)
            return Error.Validation("Inductions.CreationDate", "CreationDate is not valid");

        if (await _inductionRepository.GetByIdAsync(new InductionId(command.InductionId)) is not Induction oldInduction)
            return Error.NotFound("Induction.NotFound", "The Induction with the provide Id was not found.");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<CollaboratorGeneralInduction> collaboratorFinishInductions = await _collaboratorGeneralInductionRepository.GetByInductionIdAsync(oldInduction.Id);

        if (collaboratorFinishInductions is not null && collaboratorFinishInductions.Count > 0)
        {
            foreach (CollaboratorGeneralInduction colInduction in collaboratorFinishInductions)
            {
                colInduction.HasInductionBeenDeletedWhenHasCompleted = true;
            }
            _collaboratorGeneralInductionRepository.UpdateRange(collaboratorFinishInductions);
        }

        oldInduction.IsInductionDeleted = true;
        oldInduction.DeleteDate = deleteDate;
        oldInduction.EmailWhoDeletedByTH = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.BusinessEmail.Value : string.Empty;

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
