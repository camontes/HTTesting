using ErrorOr;
using HR_Platform.Application.Inductions.Common;
using HR_Platform.Application.Inductions.InductionCompletedSearchFilter;
using HR_Platform.Application.SearchFilters.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorGeneralInductions;
using HR_Platform.Domain.CollaboratorInductions;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.InductionFiles;
using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.SearchFilters;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Inductions.InductionSentSearchFilter;

internal sealed class InductionsCompletedSearchFilterQueryHandler
(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorGeneralInductionRepository collaboratorGeneralInductionRepository,
    ICollaboratorInductionRepository collaboratorInductionRepository,
    IInductionRepository inductionRepository,

    ICalculateTimeDifference calculateTimeDifference,
    ITimeFormatService timeFormatService
)
:
IRequestHandler<InductionsCompletedSearchFilterQuery, ErrorOr<SearchFilterResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorGeneralInductionRepository _collaboratorGeneralInductionRepository = collaboratorGeneralInductionRepository ?? throw new ArgumentNullException(nameof(collaboratorGeneralInductionRepository));
    private readonly ICollaboratorInductionRepository _collaboratorInductionRepository = collaboratorInductionRepository ?? throw new ArgumentNullException(nameof(collaboratorInductionRepository));
    private readonly IInductionRepository _inductionRepository = inductionRepository ?? throw new ArgumentNullException(nameof(inductionRepository));

    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<SearchFilterResponse>> Handle(InductionsCompletedSearchFilterQuery query, CancellationToken cancellationToken)
    {

        SearchFilter<CollaboratorGeneralInduction> results = await _collaboratorGeneralInductionRepository.SearchAsync
        (
            new BasicRequestSearch { Query = query.Query, Page = query.Page, PageSize = query.PageSize }
        );

        List<CollaboratorGeneralInduction> collaboratorInduction = results.Items.ToList();

        List<InductionCompletedResponse> listResponse = [];

        if (collaboratorInduction is not null && collaboratorInduction.Count > 0)
        {
            var collaboratorInductionByCollaborator = collaboratorInduction.GroupBy(x => x.Collaborator.Id);

            foreach (var group in collaboratorInductionByCollaborator)
            {
                Collaborator tempCollaborator = group.First().Collaborator;

                List<InductionCompletedHistoryResponse> inductionCompletedHistory = [];

                foreach (CollaboratorGeneralInduction inductionFinished in group)
                {
                    List<InductionFileResponse> filesList = [];

                    if (!inductionFinished.Induction.IsInductionDeleted)
                    {
                        foreach (InductionFile file in inductionFinished.Induction.InductionFiles)
                        {
                            InductionFileResponse fileTemp = new
                            (
                                file.Id.Value,
                            file.FileName,
                            file.UrlFile,
                                String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", inductionFinished.CreationDate.Value).Split('.')[0]), // TimePosted
                                String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", inductionFinished.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
                                _timeFormatService.GetDateFormatMonthLarge(inductionFinished.CreationDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // CreationDate,
                                _timeFormatService.GetDateFormatMonthLarge(inductionFinished.CreationDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")), // CreationDateEnglish,
                                _timeFormatService.GetDateTimeFormatMonthToltip(inductionFinished.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")) // CreationDateTooltip,
                            );
                            filesList.Add(fileTemp);
                        }
                    }

                    Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(inductionFinished.Induction.EmailWhoDeletedByTH);

                    InductionCompletedHistoryResponse temp = new
                    (
                       inductionFinished.Id.Value.ToString(),
                       inductionFinished.Induction.Name,
                       !inductionFinished.Induction.IsInductionDeleted ? inductionFinished.Induction.Description : string.Empty,
                       _timeFormatService.GetDateFormatMonthLarge(inductionFinished.CreationDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // FinishFormat,
                       _timeFormatService.GetDateFormatMonthLarge(inductionFinished.CreationDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")), // FinishFormatEnglish,
                       _timeFormatService.GetDateTimeFormatMonthToltip(inductionFinished.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // FinishFormatTooltip,
                       !inductionFinished.Induction.IsInductionDeleted ? _timeFormatService.GetDateFormatMonthLarge(inductionFinished.Induction.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")) : string.Empty, // UpdatedFormat,
                       !inductionFinished.Induction.IsInductionDeleted ? _timeFormatService.GetDateFormatMonthLarge(inductionFinished.Induction.EditionDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")) : string.Empty, // UpdatedFormatEnglish,
                       !inductionFinished.Induction.IsInductionDeleted ? _timeFormatService.GetDateTimeFormatMonthToltip(inductionFinished.Induction.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")) : string.Empty, // UpdatedFormatTooltip,
                       inductionFinished.Induction.IsInductionDeleted ? _timeFormatService.GetDateFormatMonthLarge(inductionFinished.Induction.DeleteDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")) : string.Empty, // DeleteFormat,
                       inductionFinished.Induction.IsInductionDeleted ? _timeFormatService.GetDateFormatMonthLarge(inductionFinished.Induction.DeleteDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")) : string.Empty, // DeleteFormatEnglish,
                       inductionFinished.Induction.IsInductionDeleted ? _timeFormatService.GetDateTimeFormatMonthToltip(inductionFinished.Induction.DeleteDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")) : string.Empty, // DeleteFormatTooltip,
                       inductionFinished.Induction.IsInductionDeleted,
                       inductionFinished.Induction.IsInductionDeleted && CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty, // NameWhoDeletedByTh
                       !inductionFinished.Induction.IsInductionDeleted ? inductionFinished.Induction.NameWhoChangedByTH : string.Empty, // FullNameTh
                       inductionFinished.CreationDate.Value,
                       filesList
                    );

                    inductionCompletedHistory.Add(temp);
                }

                InductionCompletedResponse inductionSentList = new
                (
                    tempCollaborator.Id.Value.ToString(),
                    tempCollaborator.DocumentType is not null ? tempCollaborator.DocumentType.Name : "C.C",
                    tempCollaborator.Document,
                    tempCollaborator.Name,
                    tempCollaborator.EntranceDate.Value,
                    _timeFormatService.GetDateFormatMonthLarge(tempCollaborator.EntranceDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // EntranceDate,
                    _timeFormatService.GetDateFormatMonthLarge(tempCollaborator.EntranceDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")), // EntranceDateEnglish
                    tempCollaborator.Assignation.Name,
                    inductionCompletedHistory.Count,
                    inductionCompletedHistory
                );

                listResponse.Add(inductionSentList);
            }
        }


        return new SearchFilterResponse
        (
            results.TotalCount,
            listResponse,
            results.TotalCount > 0 ? "Resultados encontrados." : "No se encontraron resultados."
        );
    }
}
