using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.ResendInvitation;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using MediatR;

namespace HR_Platform.Application.Companies.ResendInvitation;

internal sealed class ResendInvitationCommandHandler : IRequestHandler<ResendInvitationCommand, ErrorOr<ResendInvitationResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository;

    private readonly IRandomService _randomService;

    public ResendInvitationCommandHandler
    (
        ICollaboratorRepository collaboratorRepository,

        IRandomService randomService
    )
    {
        _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));

        _randomService = randomService ?? throw new ArgumentNullException(nameof(randomService));
        _randomService = randomService ?? throw new ArgumentNullException(nameof(randomService));
    }

    public async Task<ErrorOr<ResendInvitationResponse>> Handle(ResendInvitationCommand query, CancellationToken cancellationToken)
    {
        string randomPassword = _randomService.GenerateRandomPasswordNineLetters();

        if (await _collaboratorRepository.GetByEmailAsync(query.BusinessEmail) is null)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Email was not found.");
        }

        return new ResendInvitationResponse
        (
            randomPassword
        );
    }
}