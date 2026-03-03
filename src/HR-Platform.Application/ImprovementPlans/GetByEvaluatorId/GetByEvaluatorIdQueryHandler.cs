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

namespace HR_Platform.Application.ImprovementPlans.GetByEvaluatorId;
internal sealed class GetByEvaluatorIdQueryHandler
(
    ICollaboratorRepository collaboratorRepository,
    IImprovementPlanRepository improvementPlanRepository,
    IImprovementPlanTaskRepository improvementPlanTaskRepository,

    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

)
:
IRequestHandler<GetByEvaluatorIdQuery, ErrorOr<ImprovementPlansByEvaluatorResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IImprovementPlanRepository _improvementPlanRepository = improvementPlanRepository ?? throw new ArgumentNullException(nameof(improvementPlanRepository));
    private readonly IImprovementPlanTaskRepository _improvementPlanTaskRepository = improvementPlanTaskRepository ?? throw new ArgumentNullException(nameof(improvementPlanTaskRepository));

    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<ImprovementPlansByEvaluatorResponse>> Handle(GetByEvaluatorIdQuery query, CancellationToken cancellationToken)
    {

        try
        {
            if (await _collaboratorRepository.GetByEmailAsync(query.EvaluatorEmail) is not Collaborator evaluator)
                return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

            SearchFilter<ImprovementPlan>? improvementPlanAnswer
            =
                await _improvementPlanRepository.GetByEvaluatorIdAndNameAsync
                (
                    new ImprovementPlansRequestSearch
                    {
                        EvaluatorId = evaluator.Id.Value,

                        WithResponses = query.WithResponses,

                        CollaboratorName = query.CollaboratorName,

                        Page = query.Page,
                        PageSize = query.PageSize
                    }
                );

            List<ImprovementPlan> improvementPlans = [.. improvementPlanAnswer.Items];

            List<ImprovementPlanByEvaluatorResponse> improvementPlansByEvaluatorResponse = [];

            if (improvementPlans is not null && improvementPlans.Count > 0)
            {
                foreach (ImprovementPlan improvementPlan in improvementPlans)
                {
                    List<ImprovementPlanTask> improvementPlanTasks = improvementPlan.ImprovementPlanTasks;

                    if (improvementPlanTasks is not null && improvementPlanTasks.Count > 0)
                    {
                        List<ImprovementPlanTaskByEvaluatorResponse> improvementPlanTasksByEvaluatorResponse = [];

                        foreach (ImprovementPlanTask improvementPlanTask in improvementPlanTasks)
                        {
                            List<ImprovementPlanFileByEvaluatorResponse> improvementPlanFilesByEvaluatorResponse = [];

                            if (improvementPlanTask.ImprovementPlanTaskFiles is not null && improvementPlanTask.ImprovementPlanTaskFiles.Count > 0)
                            {
                                foreach (ImprovementPlanTaskFile file in improvementPlanTask.ImprovementPlanTaskFiles)
                                {
                                    ImprovementPlanFileByEvaluatorResponse improvementPlanFileByEvaluatorResponse = new
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

                                    improvementPlanFilesByEvaluatorResponse.Add(improvementPlanFileByEvaluatorResponse);
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

                            ImprovementPlanTaskByEvaluatorResponse improvementPlanTaskResponse = new
                            (
                                improvementPlanTask.Id.Value,

                                improvementPlanTask.TaskDescription,

                                !string.IsNullOrEmpty(improvementPlanTaskResponseByCollaboratorResponse.ImprovementPlanTaskResponseDescription)
                                    ? improvementPlanTaskResponseByCollaboratorResponse : null,

                                improvementPlanFilesByEvaluatorResponse
                            );

                            improvementPlanTasksByEvaluatorResponse.Add(improvementPlanTaskResponse);
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

                        ImprovementPlanByEvaluatorResponse improvementPlanByEvaluatorResponse = new
                        (
                            improvementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluatedId.Value,

                            improvementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.Name,
                            _stringService.GetInitials(improvementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.Name),

                            improvementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.Photo,

                            documentTypeCollaborator,
                            documentTypeEnglishCollaborator,
                            improvementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.Document,

                            improvementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.Position.Name,
                            improvementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.Position.NameEnglish,

                            _timeFormatService
                                .GetDateFormatMonthShort(improvementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.EntranceDate.Value,
                                "dd MMM yyyy", new CultureInfo("es-CO")), // EntranceDate,
                            _timeFormatService
                                .GetDateFormatMonthShort(improvementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.EntranceDate.Value,
                                "MMM dd yyyy", new CultureInfo("en-US")), // EntranceDateEnglish,

                            improvementPlan.CollaboratorCriteriaAnswer.ReferenceNumber,

                            string.Join(" ", _calculateTimeDifference
                                .CalculateTimeDifferenceFunction("Agregado", "Added", improvementPlan.CollaboratorCriteriaAnswer.CreationDate.Value).Split('.')[0]), // AddedTimeFormat
                            string.Join(" ", _calculateTimeDifference
                                .CalculateTimeDifferenceFunction("Agregado", "Added", improvementPlan.CollaboratorCriteriaAnswer.CreationDate.Value).Split('.')[1]), // AddedTimeFormatEnglish
                            _timeFormatService
                                .GetDateTimeFormatMonthToltip(improvementPlan.CollaboratorCriteriaAnswer.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // AddedTimeFormatToltip,

                            improvementPlan.Id.Value,

                            generalScoreResult > 79 ? "G" : generalScoreResult > 49 ? "Y" : "R",
                            generalScoreResult.ToString(),

                            improvementPlanTasksByEvaluatorResponse
                        );

                        improvementPlansByEvaluatorResponse.Add(improvementPlanByEvaluatorResponse);
                    }
                }
            }

            string documentType = evaluator.DocumentType is not null && evaluator.DocumentType.Id.Value != 8
                ? evaluator.DocumentType.Name : string.Empty;

            string documentTypeEnglish = evaluator.DocumentType is not null && evaluator.DocumentType.Id.Value != 8
                ? evaluator.DocumentType.NameEnglish : string.Empty;

            ImprovementPlansByEvaluatorResponse response = new
            (
                evaluator.Id.Value,

                evaluator.Name,
                _stringService.GetInitials(evaluator.Name),

                evaluator.Photo,

                documentType,
                documentTypeEnglish,

                evaluator.Document,

                evaluator.Position.Name,
                evaluator.Position.NameEnglish,

                _timeFormatService.GetDateFormatMonthShort(evaluator.EntranceDate.Value, "dd MMM yyyy", new CultureInfo("es-CO")), // EntranceDate,
                _timeFormatService.GetDateFormatMonthShort(evaluator.EntranceDate.Value, "MMM dd yyyy", new CultureInfo("en-US")), // EntranceDateEnglish,

                improvementPlansByEvaluatorResponse
            );

            return response;
        }
        catch(Exception ex)
        {
            return new ErrorOr<ImprovementPlansByEvaluatorResponse>();
        }
    }
}