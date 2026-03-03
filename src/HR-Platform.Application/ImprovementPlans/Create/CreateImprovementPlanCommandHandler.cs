using ErrorOr;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Domain.CollaboratorCriteriaAnswers;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.ImprovementPlans;
using HR_Platform.Domain.ImprovementPlanTaskFiles;
using HR_Platform.Domain.ImprovementPlanTasks;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.ImprovementPlans.Create;

internal sealed class CreateImprovementPlansCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IImprovementPlanRepository improvementPlanRepository,
    IImprovementPlanTaskRepository improvementPlanTaskRepository,
    IImprovementPlanTaskFileRepository improvementPlanTaskFileRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateImprovementPlanCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IImprovementPlanRepository _improvementPlanRepository = improvementPlanRepository ?? throw new ArgumentNullException(nameof(improvementPlanRepository));
    private readonly IImprovementPlanTaskRepository _improvementPlanTaskRepository = improvementPlanTaskRepository ?? throw new ArgumentNullException(nameof(improvementPlanTaskRepository));
    private readonly IImprovementPlanTaskFileRepository _improvementPlanTaskFileRepository = improvementPlanTaskFileRepository ?? throw new ArgumentNullException(nameof(improvementPlanTaskFileRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateImprovementPlanCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("ImprovementPlans.CreationDate", "CreationDate is not valid");

        int countTasks = await _improvementPlanTaskRepository.CountTasksByCollaboratorAsync(new CollaboratorCriteriaAnswerId(command.CollaboratorCriteriaAnswerId));

        if (countTasks + command.ImprovementPlanObjects.Count > 10)
            return Error.Validation("ImprovementPlans.NumberOfTasks", "The Collaborator may not have more than 10 tasks assigned to him/her.");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<ImprovementPlanTask> improvementPlanTaskTasks = [];
        List<ImprovementPlanTaskFile> improvementPlanTaskFiles = [];

        ImprovementPlanId improvementPlanId = new(Guid.NewGuid());

        _improvementPlanRepository.Add
        (
            new
            (
                improvementPlanId,

                new CollaboratorCriteriaAnswerId(command.CollaboratorCriteriaAnswerId),

                true, // IsEditable
                true, // IsDeleteable


                creationDate, // CreationDate
                creationDate // EditionDate
            )
        );

        foreach (CreateImprovementPlanObject item in command.ImprovementPlanObjects)
        {
            ImprovementPlanTask improvementPlanTask = new
            (
                new ImprovementPlanTaskId(Guid.NewGuid()),
                improvementPlanId,
                item.TaskDescription,
                true, //IsEditable
                true, //IsDeletable
                creationDate,
                creationDate
            );
            improvementPlanTaskTasks.Add(improvementPlanTask);

            foreach (CreateImprovementPlanFiles file in item.ImprovementPlansFiles)
            {
                ImprovementPlanTaskFile tempFile = new
                (
                    new ImprovementPlanTaskFileId(Guid.NewGuid()),
                    improvementPlanTask.Id,
                    file.FileName,
                    file.UrlFile,
                    command.EmailChangeBy,
                    CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty,
                    CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Photo : string.Empty,
                    true, //IsEditable
                    true, //IsDeletable
                    creationDate,
                    creationDate
                );
                improvementPlanTaskFiles.Add(tempFile);
            }
        }

        if (improvementPlanTaskTasks.Count > 0)
        {
            _improvementPlanTaskRepository.AddRange(improvementPlanTaskTasks);
            _improvementPlanTaskFileRepository.AddRange(improvementPlanTaskFiles);
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