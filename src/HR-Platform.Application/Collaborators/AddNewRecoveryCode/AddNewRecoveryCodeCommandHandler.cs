using ErrorOr;
using HR_Platform.Application.Collaborators.AddNewRecoveryCode;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.Companies.AddNewRecoveryCode;

internal sealed class AddNewRecoveryCodeCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IRandomService randomService,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddNewRecoveryCodeCommand, ErrorOr<RecoveryCodeResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));

    private readonly IRandomService _randomService = randomService ?? throw new ArgumentNullException(nameof(randomService));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<RecoveryCodeResponse>> Handle(AddNewRecoveryCodeCommand query, CancellationToken cancellationToken)
    {
        string recoveryCode = _randomService.GetRandomRecoveryCode();
        bool isSuspended;

        if (await _collaboratorRepository.GetByEmailAsync(query.Email) is not Collaborator collaborator)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Email was not found.");
        }
        else
        {
            collaborator.RecoveryCode = recoveryCode;

            isSuspended = collaborator.IsSuspended;

            _collaboratorRepository.Update(collaborator);
        }

        if (!isSuspended)
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        else
            recoveryCode = string.Empty;

        return new RecoveryCodeResponse
        (
            recoveryCode,
            isSuspended
        );
    }
}