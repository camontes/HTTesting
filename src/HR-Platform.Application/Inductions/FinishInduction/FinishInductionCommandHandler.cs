using ErrorOr;
using HR_Platform.Domain.CollaboratorGeneralInductions;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Inductions.FinishInduction;

internal sealed class FinishInductionCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorGeneralInductionRepository collaboratorGeneralInductionRepository,
    IInductionRepository inductionRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<FinishInductionCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorGeneralInductionRepository _collaboratorGeneralInductionRepository = collaboratorGeneralInductionRepository ?? throw new ArgumentNullException(nameof(collaboratorGeneralInductionRepository));
    private readonly IInductionRepository _inductionRepository = inductionRepository ?? throw new ArgumentNullException(nameof(inductionRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(FinishInductionCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Inductions.CreationDate", "CreationDate is not valid");

        if (await _collaboratorRepository.GetByEmailAsync(command.CollaboratorEmail) is not Collaborator CollaboratorWhoChanged)
            return Error.Validation("Inductions.CollaboratorEmail", "The collaborator with the provide Email was not found");

        if (await _inductionRepository.GetByIdAsync(new InductionId(command.InductionId)) is not Induction oldInduction)
            return Error.Validation("Inductions.InductionId", "The Induction with the provide Email was not found");

        CollaboratorGeneralInduction response = new
        (
            new CollaboratorGeneralInductionId(Guid.NewGuid()),
            CollaboratorWhoChanged.Id,
            oldInduction.Id,
            false, //HasInductionBeenDeletedWhenHasCompleted
            true, // IsEditable
            true, // IsDeleteable
            creationDate,
            creationDate
        );

        _collaboratorGeneralInductionRepository.Add(response);

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
