using ErrorOr;
using HR_Platform.Application.WorkplaceInformations.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using HR_Platform.Domain.WorkplaceInformations;
using MediatR;

namespace HR_Platform.Application.WorkplaceInformations.Create;

internal sealed class CreateWorkplaceInformationsCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IWorkplaceInformationRepository WorkplaceInformationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateWorkplaceInformationsCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IWorkplaceInformationRepository _workplaceInformationRepository = WorkplaceInformationRepository ?? throw new ArgumentNullException(nameof(WorkplaceInformationRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateWorkplaceInformationsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(command.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("WorkplaceInformations.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<WorkplaceInformation> workplaceInformationList = [];

        foreach (FileWorkplaceInformationFormatResponse item in command.FormatFiles)
        {
            WorkplaceInformation result = new
            (
                new WorkplaceInformationId(Guid.NewGuid()),
                oldCollaborator.Id,
                item.FileName,
                item.FileURL,
                command.EmailChangeBy,
                CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty,
                CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Photo : string.Empty,
                true, //IsEditable
                true, //IsDeletable
                creationDate,
                creationDate
            );
            workplaceInformationList.Add(result);
        }
        if (workplaceInformationList.Count > 0)
        {
            _workplaceInformationRepository.AddRange(workplaceInformationList);
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