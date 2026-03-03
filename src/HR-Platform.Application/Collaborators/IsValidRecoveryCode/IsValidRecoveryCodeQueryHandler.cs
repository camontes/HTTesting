using ErrorOr;
using HR_Platform.Application.Common;
using HR_Platform.Domain.Collaborators;
using MediatR;

namespace HR_Platform.Application.Collaborators.IsValidRecoveryCode;

internal sealed class IsValidRecoveryCodeQueryHandler(
    ICollaboratorRepository collaboratorRepository
    ) : IRequestHandler<IsValidRecoveryCodeQuery, ErrorOr<BooleanExistsResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));

    public async Task<ErrorOr<BooleanExistsResponse>> Handle(IsValidRecoveryCodeQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByEmailAsync(query.Email.Value) is not Collaborator collaborator)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");
        }

        if(collaborator.RecoveryCode != query.RecoveryCode)
        {
            return Error.NotFound("Collaborator.NotMatch", "The recovery code is wrong.");
        }

        BooleanExistsResponse booleanExistsResponse = new(
            true
         );

        return booleanExistsResponse;
    }
}