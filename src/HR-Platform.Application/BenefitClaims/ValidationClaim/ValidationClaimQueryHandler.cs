using ErrorOr;
using HR_Platform.Domain.Benefits;
using HR_Platform.Domain.CollaboratorBenefitClaims;
using HR_Platform.Domain.Collaborators;
using MediatR;

namespace HR_Platform.Application.BenefitClaims.ValidationClaim;
internal sealed class ValidationClaimQueryHandler(
    ICollaboratorBenefitClaimRepository collaboratorBenefitClaimRepository,
    IBenefitRepository benefitRepository,
    ICollaboratorRepository collaboratorRepository
    ) : IRequestHandler<ValidationClaimQuery, ErrorOr<bool>>
{
    private readonly ICollaboratorBenefitClaimRepository _collaboratorBenefitClaimRepository = collaboratorBenefitClaimRepository ?? throw new ArgumentNullException(nameof(collaboratorBenefitClaimRepository));
    private readonly IBenefitRepository _benefitRepository = benefitRepository ?? throw new ArgumentNullException(nameof(benefitRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    public async Task<ErrorOr<bool>> Handle(ValidationClaimQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByEmailAsync(query.CollaboratorEmail) is not Collaborator oldCollaborator)
            return Error.Validation("BenefitClaim.CollaboratorId", "Collaborator with the provide email was not found");

        if (await _benefitRepository.GetByIdAsync(new BenefitId(query.BenefitId)) is not Benefit oldBenefit)
            return Error.Validation("BenefitClaim.BenefitId", "Benefit with the provide Id was not found");

        CollaboratorBenefitClaim? collaboratorBenefit = await _collaboratorBenefitClaimRepository.ValidateClaimAsync(oldBenefit.Id, oldCollaborator.Id);
        bool canApplyAgain = false;

        //Tiene una solicitud pendiente
        if (collaboratorBenefit is not null)
        {
            canApplyAgain = oldBenefit.EditionDate.Value > collaboratorBenefit.EditionDate.Value;
        }

        return canApplyAgain;
    }
}