using ErrorOr;
using HR_Platform.Application.DreamMapAnswers.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorDreamMapAnswers;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DreamMapAnswers;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.DreamMapAnswers.GetByCollaboratorId;

internal sealed class GetDreamMapAnswersByCollaboratorIdHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorDreamMapAnswerRepository collaboratorDreamMapAnswerRepository,
    ICalculateTimeDifference calculateTimeDifference,
    ITimeFormatService timeFormatService

    ) : IRequestHandler<GetDreamMapAnswersByCollaboratorIdQuery, ErrorOr<DreamMapAnswersResponse>>
{
    private readonly ICollaboratorDreamMapAnswerRepository _collaboratorDreamMapAnswerRepository = collaboratorDreamMapAnswerRepository ?? throw new ArgumentNullException(nameof(collaboratorDreamMapAnswerRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<DreamMapAnswersResponse>> Handle(GetDreamMapAnswersByCollaboratorIdQuery query, CancellationToken cancellationToken)
    {
        Collaborator? oldCollaborator;
        if (query.IsByEmail)
        {
            oldCollaborator = await _collaboratorRepository.GetByEmailAsync(query.CollaboratorEmailOrId);
            if (oldCollaborator is null)
                return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide email was not found.");
        }
        else
        {
            oldCollaborator = await _collaboratorRepository.GetByIdAsync(new CollaboratorId(Guid.TryParse(query.CollaboratorEmailOrId, out Guid temp) ? temp : Guid.NewGuid()));
            if (oldCollaborator is null)
                return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide email was not found.");
        }

        CollaboratorDreamMapAnswer? dreamMapAnswers = await _collaboratorDreamMapAnswerRepository.GetByCollaboratorIdAsync(oldCollaborator.Id);

        List<DreamMapAnswersAllResponse> dreamMapAnswersResponse = [];
        int indicator = 0;
        bool saveCurrent = true;
        string collaboratorName = string.Empty;
        DateTime DateCompletionForm = DateTime.MinValue;

        if (dreamMapAnswers is not null)
        {
            foreach (DreamMapAnswer dreamMapAnswer in dreamMapAnswers.DreamMapAnswers)
            {
                dreamMapAnswersResponse.Add
                (
                    new DreamMapAnswersAllResponse
                    (
                        dreamMapAnswer.Id.Value,
                        dreamMapAnswers.CollaboratorId.Value.ToString(),
                        dreamMapAnswer.Question,
                        dreamMapAnswer.Answer
                    )
                );
            }
            indicator = dreamMapAnswers.TemplateIndicator;
            saveCurrent = dreamMapAnswers.SaveCurrent;
            collaboratorName = dreamMapAnswers.Collaborator.Name;
            DateCompletionForm = dreamMapAnswers.CreationDate.Value;
        }

        DreamMapAnswersResponse result = new
        (
            dreamMapAnswers is not null ? dreamMapAnswers.Id.Value.ToString() : string.Empty,
            dreamMapAnswersResponse,
            indicator,
            collaboratorName,
            DateCompletionForm != DateTime.MinValue ? _timeFormatService.GetDateFormatMonthLarge(DateCompletionForm, "dd MMMM yyyy", new CultureInfo("es-CO")) : string.Empty,
            DateCompletionForm != DateTime.MinValue ? _timeFormatService.GetDateFormatMonthLarge(DateCompletionForm, "MMMM dd, yyyy", new CultureInfo("en-US")) : string.Empty,
            saveCurrent
        );

        return result;

    }
}