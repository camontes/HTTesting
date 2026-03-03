using ErrorOr;
using HR_Platform.Application.WorkplaceRecommendations.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using HR_Platform.Domain.WorkplaceRecommendations;
using MediatR;

namespace HR_Platform.Application.WorkplaceRecommendations.Create;

internal sealed class CreateWorkplaceRecommendationsCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IWorkplaceRecommendationRepository WorkplaceRecommendationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateWorkplaceRecommendationsCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IWorkplaceRecommendationRepository _workplaceRecommendationRepository = WorkplaceRecommendationRepository ?? throw new ArgumentNullException(nameof(WorkplaceRecommendationRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateWorkplaceRecommendationsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(command.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("WorkplaceRecommendations.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<WorkplaceRecommendation> workplaceRecommendationList = [];

        foreach (FileWorkplaceRecommendationFormatResponse item in command.FormatFiles)
        {
            WorkplaceRecommendation result = new
            (
                new WorkplaceRecommendationId(Guid.NewGuid()),
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
            workplaceRecommendationList.Add(result);
        }
        if (workplaceRecommendationList.Count > 0)
        {
            _workplaceRecommendationRepository.AddRange(workplaceRecommendationList);
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