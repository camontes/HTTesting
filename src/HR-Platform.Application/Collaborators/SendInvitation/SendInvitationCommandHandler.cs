using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.SendInvitation;
using HR_Platform.Application.ServicesInterfaces;
using MediatR;

namespace HR_Platform.Application.Companies.SendInvitation;

internal sealed class SendInvitationCommandHandler(
    IRandomService randomService
    ) : IRequestHandler<SendInvitationCommand, ErrorOr<SendInvitationResponse>>
{
    private readonly IRandomService _randomService = randomService ?? throw new ArgumentNullException(nameof(randomService));

    public Task<ErrorOr<SendInvitationResponse>> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
    {
        string randomPassword = _randomService.GenerateRandomPasswordNineLetters();

        return Task.FromResult<ErrorOr<SendInvitationResponse>>(new SendInvitationResponse
        (
            randomPassword
        ));
    }
}