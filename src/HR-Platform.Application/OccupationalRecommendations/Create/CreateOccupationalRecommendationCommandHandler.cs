using ErrorOr;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Application.OccupationalRecommendations.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.OccupationalRecommendations;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.OccupationalRecommendations.Create;

internal sealed class CreateOccupationalRecommendationsCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IOccupationalRecommendationRepository OccupationalRecommendationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateOccupationalRecommendationsCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IOccupationalRecommendationRepository _occupationalRecommendationRepository = OccupationalRecommendationRepository ?? throw new ArgumentNullException(nameof(OccupationalRecommendationRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateOccupationalRecommendationsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(command.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("OccupationalRecommendations.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<OccupationalRecommendation> occupationalRecommendationList = [];

        foreach (FileOccupationalRecommendationFormatResponse item in command.FormatFiles)
        {
            OccupationalRecommendation result = new
            (
                new OccupationalRecommendationId(Guid.NewGuid()),
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
            occupationalRecommendationList.Add(result);
        }
        if (occupationalRecommendationList.Count > 0)
        {
            _occupationalRecommendationRepository.AddRange(occupationalRecommendationList);
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