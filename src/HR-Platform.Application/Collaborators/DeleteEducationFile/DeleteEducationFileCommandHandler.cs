using ErrorOr;
using HR_Platform.Domain.CollaboratorEducations;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Collaborators.DeleteEducationFile;


internal sealed class DeleteEducationFileCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorEducationRepository collaboratorEducationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteEducationFileCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorEducationRepository _collaboratorEducationRepository = collaboratorEducationRepository ?? throw new ArgumentNullException(nameof(collaboratorEducationRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    public async Task<ErrorOr<bool>> Handle(DeleteEducationFileCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(command.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

        if (await _collaboratorEducationRepository.GetByIdAsync(new CollaboratorEducationId(command.CollaboratorEducationId)) is not CollaboratorEducation oldCollaboratorEducation)
            return Error.NotFound("Collaborator.NotFound", "The Collaborator Education with the provide Id was not found.");

        oldCollaborator.EditionDate = editionDate;

        _collaboratorEducationRepository.Delete(oldCollaboratorEducation);
        _collaboratorRepository.Update(oldCollaborator);

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