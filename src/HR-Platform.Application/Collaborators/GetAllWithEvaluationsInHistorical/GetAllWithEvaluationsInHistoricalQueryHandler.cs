using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorCriteriaAnswers;
using HR_Platform.Domain.CollaboratorCriterias;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Collaborators.GetAllWithEvaluationsInHistorical;

internal sealed class GetAllWithEvaluationsInHistoricalQueryHandler
(
    ICollaboratorRepository collaboratorRepository,

    ITimeFormatService timeFormatService,
    IStringService stringService,
    ICalculateTimeDifference calculateTimeDifference
)
:
    IRequestHandler<GetAllWithEvaluationsInHistoricalQuery, ErrorOr<List<CollaboratorWithEvaluationsResponse>>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));

    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<List<CollaboratorWithEvaluationsResponse>>> Handle(GetAllWithEvaluationsInHistoricalQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetAllCollaboratorsWithEvaluationsInHistorical(new CompanyId(query.companyId)) is not List<Collaborator> collaborators)
        {
            return Error.NotFound("Collaborators.NotFound", "The collaborators with the provide Id was not found.");
        }

        List<CollaboratorWithEvaluationsResponse> collaboratorsReponse = [];

        try
        {
            if (collaborators is not null && collaborators.Count > 0)
            {
                foreach (Collaborator collaborator in collaborators)
                {
                    List<CriteriaResultByCollaboratorResponse> evaluationsResponse = [];

                    if (collaborator.CollaboratorCriterias is not null && collaborator.CollaboratorCriterias.Count > 0)
                    {
                        foreach (CollaboratorCriteria criteria in collaborator.CollaboratorCriterias)
                        {
                            List<CriteriaAnswer> answerObjetives = [];
                            List<CriteriaAnswer> answerSubjetives = [];

                            double ScoreTotalObjetive = 0;
                            double ScoreTotalSubjetive = 0;

                            int generalScoreTotalObjetive = 0;
                            int generalScoreTotalSubjetive = 0;

                            double generalScoreResult = 0;

                            string collaboratorCriteriaAnswerId = string.Empty;

                            if (criteria.CollaboratorCriteriaAnswers is not null && criteria.CollaboratorCriteriaAnswers.Count > 0)
                            {
                                foreach (CollaboratorCriteriaAnswer answer in criteria.CollaboratorCriteriaAnswers)
                                {
                                    CriteriaAnswer tempAnswer = new
                                    (
                                        answer.CriteriaName,
                                        answer.CriteriaNameEnglish,
                                        answer.CriteriaPercentage,
                                        answer.CriteriaScorePercentage,
                                        answer.CriteriaScoreIndexAnswerr == 1
                                            ? "R"
                                            : answer.CriteriaScoreIndexAnswerr == 2
                                            ? "Y" : answer.CriteriaScoreIndexAnswerr == 3
                                            ? "O" : "G",
                                        answer.CriteriaScoreIndexAnswerr,
                                        answer.CriteriaScoreName,
                                        answer.CriteriaScoreNameEnglish
                                    );

                                    if (answer.EvaluationCriteriaTypeId.Value == 1)
                                    {
                                        answerObjetives.Add(tempAnswer);
                                        ScoreTotalObjetive += (double)answer.CriteriaScorePercentage * answer.CriteriaPercentage / 100;
                                    }
                                    else if (answer.EvaluationCriteriaTypeId.Value == 2)
                                    {
                                        answerSubjetives.Add(tempAnswer);
                                        ScoreTotalSubjetive += (double)answer.CriteriaScorePercentage * answer.CriteriaPercentage / 100;
                                    }
                                    if (string.IsNullOrEmpty(collaboratorCriteriaAnswerId))
                                    {
                                        collaboratorCriteriaAnswerId = answer.Id.Value.ToString();
                                    }
                                }

                                generalScoreTotalObjetive =
                                    criteria.CollaboratorCriteriaAnswers.Count > 0 ? criteria.CollaboratorCriteriaAnswers[0].GeneralObjetiveCriteriaPercentage : 0;

                                CriteriaResult criteriaObjetiveResultTemp = new
                                (
                                    generalScoreTotalObjetive,
                                    Math.Round((ScoreTotalObjetive * generalScoreTotalObjetive / 100), 2),
                                    Math.Round(ScoreTotalObjetive, 2),
                                    answerObjetives
                                );

                                generalScoreTotalSubjetive =
                                    criteria.CollaboratorCriteriaAnswers.Count > 0 ? criteria.CollaboratorCriteriaAnswers[0].GeneralSubjetiveCriteriaPercentage : 0;

                                CriteriaResult criteriaSubjetiveResultTemp = new
                                (
                                    generalScoreTotalSubjetive,
                                    Math.Round((ScoreTotalSubjetive * generalScoreTotalSubjetive / 100), 2),
                                    Math.Round(ScoreTotalSubjetive, 2),
                                    answerSubjetives
                                );

                                generalScoreResult = Math.Round(criteriaObjetiveResultTemp.CriteriaPercentageTotal + criteriaSubjetiveResultTemp.CriteriaPercentageTotal, 2);

                                CriteriaResultByCollaboratorResponse evaluationResponse = new
                                (
                                    collaboratorCriteriaAnswerId,

                                    criteria.Id.Value,
                                    criteria.CollaboratorCriteriaAnswers[0].ReferenceNumber,
                                    generalScoreResult,
                                    generalScoreResult > 79 ? "G" : generalScoreResult > 49 ? "Y" : "R",
                                    criteria.Evaluator.Name,
                                    _stringService.GetInitials(criteria.Evaluator.Name),
                                        criteria.Evaluator.Photo,
                                    string.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction
                                        ("Agregado", "Added", criteria.CollaboratorCriteriaAnswers[0].CreationDate.Value).Split('.')[0]), // AddedTimeFormat
                                    string.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction
                                        ("Agregado", "Added", criteria.CollaboratorCriteriaAnswers[0].CreationDate.Value).Split('.')[1]), // AddedTimeFormatEnglish
                                    _timeFormatService.GetDateTimeFormatMonthToltip
                                        (criteria.CollaboratorCriteriaAnswers[0].CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // AddedTimeFormatToltip,
                                    criteria.Position.Name,
                                    criteria.Position.NameEnglish,
                                    criteria.CollaboratorCriteriaAnswers[0].Comments,

                                    criteriaObjetiveResultTemp,
                                    criteriaSubjetiveResultTemp,

                                    criteria.CollaboratorCriteriaAnswers[0].ImprovementPlans.Count > 0, // RequireImprovementPlan
                                    criteria.CollaboratorCriteriaAnswers[0].CreationDate.Value // CreaationDate    
                                );

                                evaluationsResponse.Add(evaluationResponse);
                            }
                        }
                    }

                    string documentType = collaborator.DocumentType is not null && collaborator.DocumentType.Id.Value != 8
                        ? collaborator.DocumentType.Name : string.Empty;

                    string documentTypeEnglish = collaborator.DocumentType is not null && collaborator.DocumentType.Id.Value != 8
                        ? collaborator.DocumentType.NameEnglish : string.Empty;

                    documentType = collaborator.DocumentType is not null && collaborator.DocumentType.Id.Value == 8 && !string.IsNullOrEmpty(collaborator.OtherDocumentType)
                        ? collaborator.OtherDocumentType : documentType;

                    documentTypeEnglish = collaborator.DocumentType is not null && collaborator.DocumentType.Id.Value == 8 && !string.IsNullOrEmpty(collaborator.OtherDocumentType)
                        ? collaborator.OtherDocumentType : documentTypeEnglish;

                    CollaboratorWithEvaluationsResponse collaboratorResponse = new
                    (
                        collaborator.Id.Value,

                        collaborator.CompanyId.Value,

                        collaborator.PositionId.Value,

                        !string.IsNullOrEmpty(collaborator.Document) ? collaborator.Document : string.Empty,
                        documentType,
                        documentTypeEnglish,

                        !string.IsNullOrEmpty(collaborator.Name) ? collaborator.Name : string.Empty,

                        collaborator.BusinessEmail is not null && !string.IsNullOrEmpty(collaborator.BusinessEmail.Value) ? collaborator.BusinessEmail.Value : string.Empty,
                        collaborator.PersonalEmail is not null && !string.IsNullOrEmpty(collaborator.PersonalEmail.Value) ? collaborator.PersonalEmail.Value : string.Empty,

                        collaborator.Position is not null && !string.IsNullOrEmpty(collaborator.Position.Name) ? collaborator.Position.Name : string.Empty,
                        collaborator.Position is not null && !string.IsNullOrEmpty(collaborator.Position.NameEnglish) ? collaborator.Position.NameEnglish : string.Empty,

                        !string.IsNullOrEmpty(collaborator.Photo) ? collaborator.Photo : string.Empty,
                        !string.IsNullOrEmpty(collaborator.PhotoName) ? collaborator.PhotoName : string.Empty,

                        evaluationsResponse,

                        collaborator.EntranceDate.Value,
                        _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "dd MMM yyyy", new CultureInfo("es-CO")),
                        _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "MMM dd yyyy", new CultureInfo("en-US")),

                        _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "dd/MMMM/yyyy", new CultureInfo("es-CO")),
                        _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "MMMM/dd/yyyy", new CultureInfo("en-US")),

                        collaborator.EditionDate.Value,
                        _timeFormatService.GetDateFormatMonthLarge(collaborator.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")),
                        _timeFormatService.GetDateFormatMonthLarge(collaborator.EditionDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")),

                        _timeFormatService.GetDateTimeFormatMonthToltip(collaborator.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")),
                        _timeFormatService.GetDateTimeFormatMonthToltip(collaborator.EditionDate.Value, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US"))
                    );

                    collaboratorsReponse.Add(collaboratorResponse);
                }
            }

            return collaboratorsReponse;
        }
        catch(Exception ex)
        {
            return collaboratorsReponse;
        }
    }
    public static string CalculateTimeDifference(DateTime fromDate)
    {
        TimeZoneInfo colombiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, colombiaTimeZone);

        TimeSpan difference = horaColombiana - fromDate;

        if (difference.TotalMinutes < 60)
        {
            return $"{(int)difference.TotalMinutes} minutos";
        }
        else if (difference.TotalHours < 24)
        {
            return $"{(int)difference.TotalHours} horas";
        }
        else if (difference.TotalDays < 30)
        {
            return $"{(int)difference.TotalDays} días";
        }
        else if (difference.TotalDays < 365)
        {
            int months = (int)(difference.TotalDays / 30);
            return $"{months} {(months == 1 ? "mes" : "meses")}";
        }
        else
        {
            int years = (int)(difference.TotalDays / 365);
            return $"{years} {(years == 1 ? "año" : "años")}";
        }
    }
}