using ErrorOr;
using HR_Platform.Domain.CollaboratorDreamMapAnswers;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.DreamMapAnswers.GetByCollaboratorId;

internal sealed class HideNotificationByQuestionUpdateQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorDreamMapAnswerRepository collaboratorDreamMapAnswerRepository,
    IUnitOfWork unitOfWork

    ) : IRequestHandler<HideNotificationByQuestionUpdateQuery, ErrorOr<bool>>
{
    private readonly ICollaboratorDreamMapAnswerRepository _collaboratorDreamMapAnswerRepository = collaboratorDreamMapAnswerRepository ?? throw new ArgumentNullException(nameof(collaboratorDreamMapAnswerRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(HideNotificationByQuestionUpdateQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByEmailAsync(query.CollaboratorEmail) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide email was not found.");

        CollaboratorDreamMapAnswer? dreamMapAnswers = await _collaboratorDreamMapAnswerRepository.GetByCollaboratorIdAsync(oldCollaborator.Id);

        if (dreamMapAnswers is not null)
        {
            dreamMapAnswers.SaveCurrent = true;
            _collaboratorDreamMapAnswerRepository.Update(dreamMapAnswers);
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