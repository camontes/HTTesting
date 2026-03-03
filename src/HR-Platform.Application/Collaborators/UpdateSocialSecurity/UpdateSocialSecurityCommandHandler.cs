using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.UpdateSocialSecurity;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.HealthEntities;
using HR_Platform.Domain.Pensions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.SeveranceBenefits;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Assignations.Update;

internal sealed class UpdateSocialSecurityCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IPensionRepository pensionRepository,
    ISeveranceBenefitRepository severanceBenefitRepository,
    IHealthEntityRepository healthEntityRepository,

IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateSocialSecurityCommand, ErrorOr<UpdateSocialSecurityResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IPensionRepository _pensionRepository = pensionRepository ?? throw new ArgumentNullException(nameof(pensionRepository));
    private readonly ISeveranceBenefitRepository _severanceBenefitRepository = severanceBenefitRepository ?? throw new ArgumentNullException(nameof(severanceBenefitRepository));
    private readonly IHealthEntityRepository _healthEntityRepository = healthEntityRepository ?? throw new ArgumentNullException(nameof(healthEntityRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<UpdateSocialSecurityResponse>> Handle(UpdateSocialSecurityCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(command.CollaboratorId)) is not Collaborator oldCollaborator)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");
        }

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Collaborators.EditionDate", "EditionDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        if (!string.IsNullOrEmpty(command.PensionId))
        {
            if (command.PensionId != "Ninguno")
            {
                if (await _pensionRepository.GetByIdAsync(new PensionId(Guid.Parse(command.PensionId))) is null)
                {
                    return Error.NotFound("Pension .NotFound", "The Pension with the provide Id was not found.");
                }
                oldCollaborator.PensionId = new PensionId(Guid.Parse(command.PensionId));
            }
            else
            {
                Pension? pension = await _pensionRepository.GetNonePensionByCompanyIdAsync(new CompanyId(Guid.Parse(command.CompanyId)));
                oldCollaborator.PensionId = pension != null ? pension.Id : new PensionId(Guid.NewGuid());
            }
        }

        if (!string.IsNullOrEmpty(command.SeveranceBenefitId))
        {
            if (command.SeveranceBenefitId != "Ninguno")
            {
                if (await _severanceBenefitRepository.GetByIdAsync(new SeveranceBenefitId(Guid.Parse(command.SeveranceBenefitId))) is null)
                {
                    return Error.NotFound("SeveranceBenefit .NotFound", "The SeveranceBenefit with the provide Id was not found.");
                }
                oldCollaborator.SeveranceBenefitId = new SeveranceBenefitId(Guid.Parse(command.SeveranceBenefitId));
            }
            else
            {
                SeveranceBenefit? severanceBenefit = await _severanceBenefitRepository.GetNoneSeveranceBenefitByCompanyIdAsync(new CompanyId(Guid.Parse(command.CompanyId)));
                oldCollaborator.SeveranceBenefitId = severanceBenefit != null ? severanceBenefit.Id : new SeveranceBenefitId(Guid.NewGuid());
            }
        }

        if (!string.IsNullOrEmpty(command.HealthEntityId))
        {
            if (command.HealthEntityId != "Ninguno")
            {
                if (await _healthEntityRepository.GetByIdAsync(new HealthEntityId(Guid.Parse(command.HealthEntityId))) is null)
                {
                    return Error.NotFound("HealthEntity .NotFound", "The HealthEntity with the provide Id was not found.");
                }

                oldCollaborator.HealthEntityId = new HealthEntityId(Guid.Parse(command.HealthEntityId));
            }
            else
            {
                HealthEntity? healthEntity = await _healthEntityRepository.GetNoneHealthEntityByCompanyIdAsync(new CompanyId(Guid.Parse(command.CompanyId)));
                oldCollaborator.HealthEntityId = healthEntity?.Id != null ? healthEntity.Id : new HealthEntityId(Guid.NewGuid());
            }
        }

        if (!string.IsNullOrEmpty(command.PensionId) || !string.IsNullOrEmpty(command.SeveranceBenefitId) || !string.IsNullOrEmpty(command.HealthEntityId))
        {
            oldCollaborator.ChangedBy = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Role.Name : oldCollaborator.ChangedBy;
            oldCollaborator.EmailChangedBy = command.EmailChangeBy;
            oldCollaborator.EditionDate = editionDate;
            _collaboratorRepository.Update(oldCollaborator);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        UpdateSocialSecurityResponse response = new(
            command.PensionId,
            command.SeveranceBenefitId,
            command.HealthEntityId,
            editionDate
            );

        return response;
    }
}