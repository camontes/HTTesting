using ErrorOr;
using HR_Platform.Application.EvaluatorCriterias.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.ImprovementPlanResponseFiles;
using HR_Platform.Domain.ImprovementPlans;
using HR_Platform.Domain.ImprovementPlanTaskFiles;
using HR_Platform.Domain.ImprovementPlanTasks;
using HR_Platform.Domain.SearchFilters;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.ImprovementPlans.GetByCollaboratorId;
internal sealed class GetByCollaboratorIdQueryHandler
(
    ICollaboratorRepository collaboratorRepository,
    IImprovementPlanRepository improvementPlanRepository,

    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

)
:
IRequestHandler<GetByCollaboratorIdQuery, ErrorOr<ImprovementPlansByCollaboratorResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IImprovementPlanRepository _improvementPlanRepository = improvementPlanRepository ?? throw new ArgumentNullException(nameof(improvementPlanRepository));

    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<ImprovementPlansByCollaboratorResponse>> Handle(GetByCollaboratorIdQuery query, CancellationToken cancellationToken)
    {

        try
        {
            if (await _collaboratorRepository.GetByEmailAsync(query.CollaboratorEmail) is not Collaborator collaborator)
                return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

            SearchFilter<ImprovementPlan>? improvementPlanAnswer
            =
                await _improvementPlanRepository.GetByCollaboratorIdAndNameAsync
                (
                    new ImprovementPlansRequestSearch
                    {
                        CollaboratorId = collaborator.Id,

                        WithResponses = query.WithResponses,

                        CollaboratorName = query.CollaboratorName,

                        Page = query.Page,
                        PageSize = query.PageSize
                    }
                );

            List<ImprovementPlan> improvementPlans = [.. improvementPlanAnswer.Items];

            List<ImprovementPlanByCollaboratorResponse> improvementPlansByCollaboratorResponse = [];

            if (improvementPlans is not null && improvementPlans.Count > 0)
            {
                foreach (ImprovementPlan improvementPlan in improvementPlans)
                {
                    List<ImprovementPlanTask> improvementPlanTasks = improvementPlan.ImprovementPlanTasks;

                    if (improvementPlanTasks is not null && improvementPlanTasks.Count > 0)
                    {
                        List<ImprovementPlanTaskByCollaboratorResponse> improvementPlanTasksByCollaboratorResponse = [];

                        foreach (ImprovementPlanTask improvementPlanTask in improvementPlanTasks)
                        {
                            List<ImprovementPlanFileByCollaboratorResponse> improvementPlanFilesByCollaboratorResponse = [];

                            if (improvementPlanTask.ImprovementPlanTaskFiles is not null && improvementPlanTask.ImprovementPlanTaskFiles.Count > 0)
                            {
                                foreach (ImprovementPlanTaskFile file in improvementPlanTask.ImprovementPlanTaskFiles)
                                {
                                    ImprovementPlanFileByCollaboratorResponse improvementPlanFileByCollaboratorResponse = new
                                    (
                                       file.Id.Value,

                                       file.FileName,

                                       file.UrlFile,

                                       file.NameWhoChangedByTH,
                                       _stringService.GetInitials(file.NameWhoChangedByTH),

                                       file.EmailWhoChangedByTH,

                                       file.UrlPhotoWhoChangedByTH,

                                       string.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", file.CreationDate.Value).Split('.')[0]), // TimePosted
                                       string.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", file.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
                                       _timeFormatService.GetDateTimeFormatMonthToltip(file.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // TimePostedTolTip
                                       _timeFormatService.GetDateTimeFormatMonthToltip(file.CreationDate.Value, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US")) // TimePostedTolTipEnglish
                                    );

                                    improvementPlanFilesByCollaboratorResponse.Add(improvementPlanFileByCollaboratorResponse);
                                }
                            }

                            ImprovementPlanTaskResponseByCollaboratorResponse improvementPlanTaskResponseByCollaboratorResponse = new
                            (
                                Guid.NewGuid(),

                                string.Empty,

                                []
                            );

                            if (improvementPlanTask.ImprovementPlanResponse is not null && improvementPlanTask.ImprovementPlanResponse.Count > 0)
                            {

                                List<ImprovementPlanTaskResponseFileByCollboratorResponse> filesResponse = [];

                                if (improvementPlanTask.ImprovementPlanResponse[0].ImprovementPlanResponseFiles is not null
                                    && improvementPlanTask.ImprovementPlanResponse[0].ImprovementPlanResponseFiles.Count > 0)
                                {
                                    foreach (ImprovementPlanResponseFile responseFile in improvementPlanTask.ImprovementPlanResponse[0].ImprovementPlanResponseFiles)
                                    {
                                        ImprovementPlanTaskResponseFileByCollboratorResponse fileResponse = new
                                        (
                                            responseFile.Id.Value,

                                            responseFile.FileName,

                                            responseFile.UrlFile,

                                            responseFile.NameWhoChanged,
                                                _stringService.GetInitials(responseFile.NameWhoChanged),

                                            responseFile.EmailWhoChanged,

                                            responseFile.UrlPhotoWhoChanged,

                                           string.Join(" ", 
                                               _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", responseFile.CreationDate.Value).Split('.')[0]), // TimePosted
                                           string.Join(" ", 
                                               _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", responseFile.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
                                           _timeFormatService.GetDateTimeFormatMonthToltip(responseFile.CreationDate.Value, 
                                               "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // TimePostedTolTip
                                           _timeFormatService.GetDateTimeFormatMonthToltip(responseFile.CreationDate.Value, 
                                               "MMM dd yyyy HH:mm tt", new CultureInfo("en-US")) // TimePostedTolTipEnglish
                                        );

                                        filesResponse.Add(fileResponse);
                                    }
                                }

                                improvementPlanTaskResponseByCollaboratorResponse = new
                                (
                                    improvementPlanTask.ImprovementPlanResponse[0].Id.Value,

                                    improvementPlanTask.ImprovementPlanResponse[0].TaskResponse,

                                    filesResponse
                                );
                            }

                            ImprovementPlanTaskByCollaboratorResponse improvementPlanTaskByCollboratorResponse = new
                            (
                                improvementPlanTask.Id.Value,

                                improvementPlanTask.TaskDescription,

                                !string.IsNullOrEmpty(improvementPlanTaskResponseByCollaboratorResponse.ImprovementPlanTaskResponseDescription)
                                    ? improvementPlanTaskResponseByCollaboratorResponse : null,

                                improvementPlanFilesByCollaboratorResponse
                            );

                            improvementPlanTasksByCollaboratorResponse.Add(improvementPlanTaskByCollboratorResponse);
                        }

                        string documentTypeCollaborator = improvementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.
                                DocumentType is not null && improvementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.DocumentType.Id.Value != 8
                                ? improvementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.DocumentType.Name : string.Empty;

                        string documentTypeEnglishCollaborator = improvementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.DocumentType is not null &&
                            improvementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.DocumentType.Id.Value != 8
                            ? improvementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.DocumentType.NameEnglish : string.Empty;

                        int generalScoreTotalObjetive = 0;
                        int generalScoreTotalSubjetive = 0;

                        double scoreTotalObjetive = 0;
                        double scoreTotalSubjetive = 0;

                        double generalScoreResult = 0;

                        generalScoreTotalObjetive = improvementPlan.CollaboratorCriteriaAnswer.GeneralObjetiveCriteriaPercentage;

                        if (improvementPlan.CollaboratorCriteriaAnswer.EvaluationCriteriaTypeId.Value == 1)
                        {
                            scoreTotalObjetive +=
                                (double)improvementPlan.CollaboratorCriteriaAnswer.CriteriaScorePercentage * improvementPlan.CollaboratorCriteriaAnswer.CriteriaPercentage / 100;
                        }
                        else if (improvementPlan.CollaboratorCriteriaAnswer.EvaluationCriteriaTypeId.Value == 2)
                        {
                            scoreTotalSubjetive +=
                                (double)improvementPlan.CollaboratorCriteriaAnswer.CriteriaScorePercentage * improvementPlan.CollaboratorCriteriaAnswer.CriteriaPercentage / 100;
                        }

                        CriteriaResult criteriaObjetiveResultTemp = new
                        (
                            generalScoreTotalObjetive,
                            Math.Round((scoreTotalObjetive * generalScoreTotalObjetive / 100), 2),
                            Math.Round(scoreTotalObjetive, 2),
                            []
                        );

                        generalScoreTotalSubjetive = improvementPlan.CollaboratorCriteriaAnswer.GeneralSubjetiveCriteriaPercentage;

                        CriteriaResult criteriaSubjetiveResultTemp = new
                        (
                            generalScoreTotalSubjetive,
                            Math.Round((scoreTotalSubjetive * generalScoreTotalSubjetive / 100), 2),
                            Math.Round(scoreTotalSubjetive, 2),
                            []
                        );

                        generalScoreResult = Math.Round(criteriaObjetiveResultTemp.CriteriaPercentageTotal + criteriaSubjetiveResultTemp.CriteriaPercentageTotal, 2);

                        ImprovementPlanByCollaboratorResponse improvementPlanByCollaboratorResponse = new
                        (
                            improvementPlan.Id.Value,

                            improvementPlan.CollaboratorCriteriaAnswer.ReferenceNumber,

                            string.Join(" ", _calculateTimeDifference
                                .CalculateTimeDifferenceFunction("Agregado", "Added", improvementPlan.CollaboratorCriteriaAnswer.CreationDate.Value).Split('.')[0]), // AddedTimeFormat
                            string.Join(" ", _calculateTimeDifference
                                .CalculateTimeDifferenceFunction("Agregado", "Added", improvementPlan.CollaboratorCriteriaAnswer.CreationDate.Value).Split('.')[1]), // AddedTimeFormatEnglish
                            _timeFormatService
                                .GetDateTimeFormatMonthToltip(improvementPlan.CollaboratorCriteriaAnswer.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // AddedTimeFormatToltip,

                            generalScoreResult > 79 ? "G" : generalScoreResult > 49 ? "Y" : "R",
                            generalScoreResult.ToString(),

                            improvementPlanTasksByCollaboratorResponse
                        );

                        improvementPlansByCollaboratorResponse.Add(improvementPlanByCollaboratorResponse);
                    }
                }
            }

            string documentType = collaborator.DocumentType is not null && collaborator.DocumentType.Id.Value != 8
                ? collaborator.DocumentType.Name : string.Empty;

            string documentTypeEnglish = collaborator.DocumentType is not null && collaborator.DocumentType.Id.Value != 8
                ? collaborator.DocumentType.NameEnglish : string.Empty;

            ImprovementPlansByCollaboratorResponse response = new
            (
                collaborator.Id.Value,

                collaborator.Name,
                _stringService.GetInitials(collaborator.Name),

                collaborator.Photo,

                documentType,
                documentTypeEnglish,

                collaborator.Document,

                collaborator.Position.Name,
                collaborator.Position.NameEnglish,

                _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "dd MMM yyyy", new CultureInfo("es-CO")), // EntranceDate,
                _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "MMM dd yyyy", new CultureInfo("en-US")), // EntranceDateEnglish,

                improvementPlansByCollaboratorResponse
            );

            return response;
        }
        catch (Exception ex)
        {
            return new ErrorOr<ImprovementPlansByCollaboratorResponse>();
        }
    }
}