using ErrorOr;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.ImprovementPlanResponseFiles;
using HR_Platform.Domain.ImprovementPlanResponses;
using HR_Platform.Domain.ImprovementPlans;
using HR_Platform.Domain.ImprovementPlanTaskFiles;
using HR_Platform.Domain.ImprovementPlanTasks;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.ImprovementPlans.CreateResponses;

internal sealed class CreateImprovementPlanResponsesCommandHandler
(
    ICollaboratorRepository collaboratorRepository,
    IImprovementPlanResponseRepository improvementPlanResponseRepository,
    IImprovementPlanResponseFileRepository improvementPlanResponseFileRepository,

    IUnitOfWork unitOfWork

)
:
IRequestHandler<CreateImprovementPlanResponsesCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IImprovementPlanResponseRepository _improvementPlanResponseRepository = 
        improvementPlanResponseRepository ?? throw new ArgumentNullException(nameof(improvementPlanResponseRepository));
    private readonly IImprovementPlanResponseFileRepository _improvementPlanResponseFileRepository =
        improvementPlanResponseFileRepository ?? throw new ArgumentNullException(nameof(improvementPlanResponseFileRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateImprovementPlanResponsesCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("ImprovementPlans.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<ImprovementPlanResponseFile> improvementPlanResponseFiles = [];

        foreach (CreateImprovementPlanResponseObject item in command.ImprovementPlanResponseObjects)
        {
            ImprovementPlanResponseId improvementPlanResponseId = new(Guid.NewGuid());

            ImprovementPlanResponse improvementPlanResponse = new
            (
                improvementPlanResponseId,

                new ImprovementPlanTaskId(item.ImprovementPlanTaskId),

                item.ResponseDescription,

                true, // IsEditable
                true, // IsDeleteable

                creationDate, // CreationDate
                creationDate // EditionDate
            );

            _improvementPlanResponseRepository.Add(improvementPlanResponse);

            foreach (CreateImprovementPlanResponseFiles file in item.ImprovementPlanResponseFiles)
            {
                ImprovementPlanResponseFile tempFile = new
                (
                    new ImprovementPlanResponseFileId(Guid.NewGuid()),

                    improvementPlanResponseId,

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

                improvementPlanResponseFiles.Add(tempFile);
            }
        }

        if (improvementPlanResponseFiles.Count > 0)
        {
            _improvementPlanResponseFileRepository.AddRange(improvementPlanResponseFiles);
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