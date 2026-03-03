using ErrorOr;
using HR_Platform.Domain.CollaboratorDreamMapAnswers;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.DreamMapAnswers.DeleteDreamMap;

internal sealed class DeleteDreamMapCommandHandler(
    ICollaboratorDreamMapAnswerRepository collaboratorDreamMapAnswerRepository,
    IUnitOfWork unitOfWork

    ) : IRequestHandler<DeleteDreamMapCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorDreamMapAnswerRepository _collaboratorDreamMapAnswerRepository = collaboratorDreamMapAnswerRepository ?? throw new ArgumentNullException(nameof(collaboratorDreamMapAnswerRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteDreamMapCommand command, CancellationToken cancellationToken)
    {
        if (await _collaboratorDreamMapAnswerRepository.GetByIdAsync(new CollaboratorDreamMapAnswerId(command.CollaboratorDreamMapAnswerId)) is not CollaboratorDreamMapAnswer oldCollaboratorDreamMapAnswer)
            return Error.NotFound("CollaboratorDreamMapAnswer.NotFound", "The Collaborator Dream Map Answer related with the provide Company Id was not found.");

        _collaboratorDreamMapAnswerRepository.Delete(oldCollaboratorDreamMapAnswer);

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