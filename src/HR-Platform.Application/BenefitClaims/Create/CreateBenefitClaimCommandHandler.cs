using ErrorOr;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Benefits;
using HR_Platform.Domain.CollaboratorBenefitClaims;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.BenefitClaims.Create;

internal sealed class CreateBenefitClaimsCommandHandler(
    ICollaboratorBenefitClaimRepository collaboratorBenefitClaimRepository,
    IBenefitRepository benefitRepository,
    ICollaboratorRepository collaboratorRepository,
    IReferenceGenerator referenceGenerator,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateBenefitClaimsCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorBenefitClaimRepository _collaboratorBenefitClaimRepository = collaboratorBenefitClaimRepository ?? throw new ArgumentNullException(nameof(collaboratorBenefitClaimRepository));
    private readonly IBenefitRepository _benefitRepository = benefitRepository ?? throw new ArgumentNullException(nameof(benefitRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IReferenceGenerator _referenceGenerator = referenceGenerator ?? throw new ArgumentNullException(nameof(referenceGenerator));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateBenefitClaimsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("BenefitClaim.CreationDate", "CreationDate is not valid");

        if (await _collaboratorRepository.GetByEmailAsync(command.CollaboratorEmail) is not Collaborator oldCollaborator)
            return Error.Validation("BenefitClaim.CollaboratorId", "Collaborator with the provide email was not found");

        if (await _benefitRepository.GetByIdAsync(new BenefitId(command.BenefitId)) is not Benefit oldBenefit)
            return Error.Validation("BenefitClaim.BenefitId", "Benefit with the provide Id was not found");

        CollaboratorBenefitClaim? collaboratorBenefit = await _collaboratorBenefitClaimRepository.ValidateClaimAsync(oldBenefit.Id, oldCollaborator.Id);

        if (collaboratorBenefit is not null)
        {
            // si la fecha de edición en beneficio es mayor que la fecha de creación de la solicitud puede volver a enviarla
            if (oldBenefit.EditionDate.Value <= collaboratorBenefit.EditionDate.Value)
            {
                return Error.Validation("BenefitClaim.Validation", "The Collaborator has already applied for this benefit");
            }
            else
            {
                collaboratorBenefit.EditionDate = creationDate;
                _collaboratorBenefitClaimRepository.Update(collaboratorBenefit);
            }
        }
        else
        {
            CollaboratorBenefitClaim benefitClaim = new
             (
               new CollaboratorBenefitClaimId(Guid.NewGuid()),
               new CompanyId(command.CompanyId),
               oldBenefit.Id,
               oldCollaborator.Id,
               _referenceGenerator.GenerateReference("B"),
               false, //IsAccepted - Validad cual de las dos opciones fueron escogidas
               false, //IsAnySelected
               true, //IsEditable
               true, //IsDeletable
               creationDate,
               creationDate
             );

            _collaboratorBenefitClaimRepository.Add(benefitClaim);
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