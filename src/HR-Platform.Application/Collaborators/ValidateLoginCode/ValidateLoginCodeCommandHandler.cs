using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.ValidateLoginCode;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.MasterUsers;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.Companies.ValidateLoginCode;

internal sealed class ValidateLoginCodeCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IMasterUserRepository masterUserRepository,

    IUnitOfWork unitOfWork
    ) : IRequestHandler<ValidateLoginCodeCommand, ErrorOr<ValidateLoginCodeResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IMasterUserRepository _masterUserRepository = masterUserRepository ?? throw new ArgumentNullException(nameof(masterUserRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<ValidateLoginCodeResponse>> Handle(ValidateLoginCodeCommand query, CancellationToken cancellationToken)
    {
        bool isValid = false;

        if (await _collaboratorRepository.GetByEmailAsync(query.Email) is not Collaborator collaborator)
        {
            if (await _masterUserRepository.GetByEmailAsync(query.Email) is not MasterUser masterUser)
            {
                return Error.NotFound("Collaborator.NotFound", "The colaborator with the provide Email was not found.");
            }

            if (masterUser.LoginCode == query.LoginCode)
                isValid = true;
        }
        else
        {
            if (collaborator.LoginCode == query.LoginCode)
                isValid = true;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ValidateLoginCodeResponse
        (
            isValid
        );
    }
}