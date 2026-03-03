using ErrorOr;
using HR_Platform.Application.WorkplaceRecommendations.Common;
using HR_Platform.Application.SearchFilters.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.WorkplaceRecommendations;
using HR_Platform.Domain.SearchFilters;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.WorkplaceRecommendations.WorkplaceRecommendationSearchFilter;

internal sealed class WorkplaceRecommendationSearchFilterQueryHandler(
    IWorkplaceRecommendationRepository WorkplaceRecommendationRepository,
    ITimeFormatService timeFormatService,
    IStringService stringService,
    ICollaboratorRepository collaboratorRepository,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<WorkplaceRecommendationSearchFilterQuery, ErrorOr<SearchFilterResponse>>
{
    private readonly IWorkplaceRecommendationRepository _workplaceRecommendationRepository = WorkplaceRecommendationRepository ?? throw new ArgumentNullException(nameof(WorkplaceRecommendationRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));

    public async Task<ErrorOr<SearchFilterResponse>> Handle(WorkplaceRecommendationSearchFilterQuery query, CancellationToken cancellationToken)
    {
        Collaborator? oldCollaborator = null;

        if (!string.IsNullOrEmpty(query.CollaboratorId))
        {
            oldCollaborator = await _collaboratorRepository.GetByIdAsync(new CollaboratorId(Guid.TryParse(query.CollaboratorId, out Guid result) ? result : Guid.NewGuid()));
        }
        else
        {
            oldCollaborator = await _collaboratorRepository.GetByEmailAsync(query.CollaboratorEmail);
        }

        if (oldCollaborator == null)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

        var results = await _workplaceRecommendationRepository.SearchAsync(new BasicRequestSearch { Query = query.Query, Page = query.Page, PageSize = query.PageSize, CollaboratorId = oldCollaborator.Id, Year = query.Year });

        List<WorkplaceRecommendationFileResponse> items = [];

        if (results.TotalCount > 0)
        {
            foreach (WorkplaceRecommendation item in results.Items)
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

                items.Add(temp);
            }
        }
        return new SearchFilterResponse
        (
            results.TotalCount,
            items,
            results.TotalCount > 0 ? "Resultados encontrados." : "No se encontraron resultados."
        );
    }
}