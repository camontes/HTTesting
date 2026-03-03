using ErrorOr;
using HR_Platform.Application.ImprovementPlans.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorCriteriaAnswers;
using HR_Platform.Domain.ImprovementPlanTaskFiles;
using HR_Platform.Domain.ImprovementPlanTasks;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.ImprovementPlans.GetByCollaboratorCriteriaAnswerId;
internal sealed class GetByCollaboratorCriteriaAnswerIdQueryHandler(
    IImprovementPlanTaskRepository improvementPlanTaskRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetByCollaboratorCriteriaAnswerIdQuery, ErrorOr<List<ImprovementPlanResponse>>>
{
    private readonly IImprovementPlanTaskRepository _improvementPlanTaskRepository = improvementPlanTaskRepository ?? throw new ArgumentNullException(nameof(improvementPlanTaskRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<List<ImprovementPlanResponse>>> Handle(GetByCollaboratorCriteriaAnswerIdQuery query, CancellationToken cancellationToken)
    {
        List<ImprovementPlanTask>? improvementPlanList = await _improvementPlanTaskRepository.GetByCollaboratorCriteriaAnswerIdAsync(new CollaboratorCriteriaAnswerId(query.CollaboratorCriteriaAnswerId));
        List<ImprovementPlanResponse> responses = [];

        if (improvementPlanList is not null && improvementPlanList.Count > 0)
        {
            foreach (ImprovementPlanTask item in improvementPlanList)
            {
                List<ImprovementPlanFileResponse> responseFiles = [];

                foreach (ImprovementPlanTaskFile file in item.ImprovementPlanTaskFiles)
                {
                    ImprovementPlanFileResponse fileTemp = new
                    (
                        file.Id.Value,
                        file.FileName, // FileName
                        file.UrlFile, // FileURL
                        String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", file.CreationDate.Value).Split('.')[0]), // TimePosted
                        String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", file.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
                        file.CreationDate.Value, // CreationDate
                        _timeFormatService.GetDateTimeFormatMonthToltip(file.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
                        file.UrlPhotoWhoChangedByTH, // UrlPhotoTH
                        file.NameWhoChangedByTH, // FullNameTh
                        _stringService.GetInitials(file.NameWhoChangedByTH) // ShortNameTh
                    );
                    responseFiles.Add(fileTemp);
                }

                ImprovementPlanResponse temp = new
                (
                   item.Id.Value, // IdFile
                   item.TaskDescription,
                   responseFiles
                );

                responses.Add(temp);
            }
        }
        return responses;
    }
}