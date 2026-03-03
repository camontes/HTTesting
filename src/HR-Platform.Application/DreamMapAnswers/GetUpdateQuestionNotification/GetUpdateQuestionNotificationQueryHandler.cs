using ErrorOr;
using HR_Platform.Domain.CollaboratorDreamMapAnswers;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DreamMapQuestions;
using MediatR;

namespace HR_Platform.Application.DreamMapAnswers.GetByCollaboratorId;

internal sealed class GetUpdateQuestionNotificationQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    IDreamMapQuestionRepository dreamMapQuestionRepository,
    ICollaboratorDreamMapAnswerRepository collaboratorDreamMapAnswerRepository

    ) : IRequestHandler<GetUpdateQuestionNotificationQuery, ErrorOr<bool>>
{
    private readonly ICollaboratorDreamMapAnswerRepository _collaboratorDreamMapAnswerRepository = collaboratorDreamMapAnswerRepository ?? throw new ArgumentNullException(nameof(collaboratorDreamMapAnswerRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IDreamMapQuestionRepository _dreamMapQuestionRepository = dreamMapQuestionRepository ?? throw new ArgumentNullException(nameof(dreamMapQuestionRepository));

    public async Task<ErrorOr<bool>> Handle(GetUpdateQuestionNotificationQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByEmailAsync(query.CollaboratorEmail) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide email was not found.");

        CollaboratorDreamMapAnswer? dreamMapAnswers = await _collaboratorDreamMapAnswerRepository.GetByCollaboratorIdAsync(oldCollaborator.Id);
        bool wasUpdate = false;

        if (dreamMapAnswers is not null)
        {
            List<DreamMapQuestion> questions = await _dreamMapQuestionRepository.GetAll();
            wasUpdate = questions.Any(x => x.CreationDate.Value > dreamMapAnswers.CreationDate.Value);
        }

        return wasUpdate;
    }
}