using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Application.WorkplaceRecommendations.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.WorkplaceRecommendations;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.WorkplaceRecommendations.GetByCollaboratorId;

internal sealed class GetWorkplaceRecommendationsByCollaboratorIdHandler(
    IWorkplaceRecommendationRepository WorkplaceRecommendationRepository,
    ICollaboratorRepository collaboratorRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetWorkplaceRecommendationsByCollaboratorIdQuery, ErrorOr<WorkplaceRecommendationsResponse>>
{
    private readonly IWorkplaceRecommendationRepository _workplaceRecommendationRepository = WorkplaceRecommendationRepository ?? throw new ArgumentNullException(nameof(WorkplaceRecommendationRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<WorkplaceRecommendationsResponse>> Handle(GetWorkplaceRecommendationsByCollaboratorIdQuery query, CancellationToken cancellationToken)
    {
        Collaborator? oldCollaborator = null;

        if (!string.IsNullOrEmpty(query.CollaboratorId))
        {
            oldCollaborator = await _collaboratorRepository.GetByIdAsync(new CollaboratorId(Guid.TryParse(query.CollaboratorId, out Guid result) ? result : Guid.NewGuid()));
        }
        else
        {
            oldCollaborator = await _collaboratorRepository.GetByEmailAsync(query.EmailChangeBy);
        }

        if (oldCollaborator == null)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

        List<WorkplaceRecommendation>? workplaceRecommendationList = await _workplaceRecommendationRepository.GetByCollaboratorIdAsync(oldCollaborator.Id, query.Year);
        List<WorkplaceRecommendationFileResponse> filesList = [];
        List<string> distinctYears = [];

        if (workplaceRecommendationList is not null && workplaceRecommendationList.Count > 0)
        {
            foreach (WorkplaceRecommendation item in workplaceRecommendationList)
            {
                WorkplaceRecommendationFileResponse temp = new
                (
                   item.Id.Value, // IdFile
                   item.FileName, // FileName
                   item.UrlFile, // FileURL
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.CreationDate.Value).Split('.')[0]), // TimePosted
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
                   item.CreationDate.Value, // CreationDate
                   _timeFormatService.GetDateTimeFormatMonthToltip(item.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
                   item.UrlPhotoWhoChangedByTH, // UrlPhotoTH
                   item.NameWhoChangedByTH, // FullNameTh
                   _stringService.GetInitials(item.NameWhoChangedByTH) // ShortNameTh
                );

                filesList.Add(temp);
            }
        }

        if (workplaceRecommendationList is not null)
        {
            distinctYears = workplaceRecommendationList
                .Select(obj => obj.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        WorkplaceRecommendationsResponse workplaceRecommendationsResponse = new
        (
            oldCollaborator.Id.Value,
            oldCollaborator.Document,
            oldCollaborator.DocumentType is not null ? oldCollaborator.DocumentType.Name : string.Empty,
            oldCollaborator.Name,
            _timeFormatService.GetDateFormatMonthShort(oldCollaborator.EntranceDate.Value, "dd MMM yyyy", new CultureInfo("es-CO")),
            [.. filesList.OrderByDescending(x => x.CreationDate)],
            distinctYears
        );

        return workplaceRecommendationsResponse;

    }
}