using ErrorOr;
using HR_Platform.Application.Collaborators.AddNewLoginCode;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DomainEmails;
using HR_Platform.Domain.MasterUsers;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using System.Text.RegularExpressions;

namespace HR_Platform.Application.Companies.AddNewLoginCode;

internal sealed class AddNewLoginCodeCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IMasterUserRepository masterUserRepository,
    IRandomService randomService,
    IDomainEmailRepository domainEmailRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddNewLoginCodeCommand, ErrorOr<LoginCodeResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IMasterUserRepository _masterUserRepository = masterUserRepository ?? throw new ArgumentNullException(nameof(masterUserRepository));
    private readonly IDomainEmailRepository _domainEmailRepository = domainEmailRepository ?? throw new ArgumentNullException(nameof(domainEmailRepository));

    private readonly IRandomService _randomService = randomService ?? throw new ArgumentNullException(nameof(randomService));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<LoginCodeResponse>> Handle(AddNewLoginCodeCommand query, CancellationToken cancellationToken)
    {
        string loginCode = _randomService.GetRandomLoginCode();
        bool alreadyLogin;
        bool isSuspended;

        //DomainEmail Validation
        const string regex = @"@(?<domain>\w+\.\w+(?:\.\w+)*)";
        Match match = Regex.Match(query.Email, regex);

        if (MailDomain.Create(match.Success ? match.Value : string.Empty) is not MailDomain domain)
            return Error.Validation("Collaborators.DomainEmail", "DomainEmail has not valid format");

        bool isDomainFound = await _domainEmailRepository.ExistsDomainNameAsync(domain);

        if (!isDomainFound)
            return Error.Validation("Collaborators.DomainEmail", "This domain is not available");

        if (await _collaboratorRepository.GetByEmailAsync(query.Email) is not Collaborator collaborator)
        {
            if (await _masterUserRepository.GetByEmailAsync(query.Email) is not MasterUser masterUser)
            {
                return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Email was not found.");
            }

            masterUser.LoginCode = loginCode;

            alreadyLogin = true;
            isSuspended = false;

            _masterUserRepository.Update(masterUser);
        }
        else
        {
            collaborator.LoginCode = loginCode;

            alreadyLogin = collaborator.AlreadyLogin;
            isSuspended = collaborator.IsSuspended;

            _collaboratorRepository.Update(collaborator);
        }

        if (!isSuspended)
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        else
            loginCode = string.Empty;

        return new LoginCodeResponse
        (
            loginCode,
            alreadyLogin,
            isSuspended
        );
    }
}