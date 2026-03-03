using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.Collaborators.UpdateEducationData;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EducationalLevels;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ProfessionalAdvices;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Assignations.Update;

internal sealed class UpdateEducationDataCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IEducationalLevelRepository educationalLevelRepository,
    IProfessionalAdviceRepository professionalAdviceRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateEducationDataCommand, ErrorOr<UpdateCollaboratorResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IEducationalLevelRepository _educationalLevelRepository = educationalLevelRepository ?? throw new ArgumentNullException(nameof(educationalLevelRepository));
    private readonly IProfessionalAdviceRepository _professionalAdviceRepository = professionalAdviceRepository ?? throw new ArgumentNullException(nameof(professionalAdviceRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<UpdateCollaboratorResponse>> Handle(UpdateEducationDataCommand command, CancellationToken cancellationToken)
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

        if (!string.IsNullOrEmpty(command.EducationalLevelId))
        {
            if (command.EducationalLevelId != "Ninguno")
            {
                if (await _educationalLevelRepository.GetByIdAsync(new EducationalLevelId(Guid.Parse(command.EducationalLevelId))) is null)
                {
                    return Error.NotFound("EducationalLevel .NotFound", "The EducationalLevel with the provide Id was not found.");
                }
                oldCollaborator.EducationalLevelId = new EducationalLevelId(Guid.Parse(command.EducationalLevelId));
            }
            else
            {
                EducationalLevel? educationalLevel = await _educationalLevelRepository.GetNoneEducationalLevelByCompanyIdAsync(new CompanyId(Guid.Parse(command.CompanyId)));
                oldCollaborator.EducationalLevelId = educationalLevel!= null ? educationalLevel.Id : new EducationalLevelId(Guid.NewGuid());
            }
        }

        if (!string.IsNullOrEmpty(command.ProfessionalAdviceId))
        {
            if (command.ProfessionalAdviceId != "Ninguno")
            {
                if (await _professionalAdviceRepository.GetByIdAsync(new ProfessionalAdviceId(Guid.Parse(command.ProfessionalAdviceId))) is null)
                {
                    return Error.NotFound("ProfessionalAdvice .NotFound", "The ProfessionalAdvice with the provide Id was not found.");
                }
                oldCollaborator.ProfessionalAdviceId = new ProfessionalAdviceId(Guid.Parse(command.ProfessionalAdviceId));
            }
            else
            {
                ProfessionalAdvice? professionalAdvice = await _professionalAdviceRepository.GetNoneProfessionalAdviceByCompanyIdAsync(new CompanyId(Guid.Parse(command.CompanyId)));
                oldCollaborator.ProfessionalAdviceId = professionalAdvice != null ? professionalAdvice.Id: new ProfessionalAdviceId(Guid.NewGuid());
            }
        }

        if (!string.IsNullOrEmpty(command.ProfessionalCard))
        {
            oldCollaborator.ProfessionalCard = command.ProfessionalCard;
        }

        if (!string.IsNullOrEmpty(command.EducationalLevelId) || !string.IsNullOrEmpty(command.ProfessionalAdviceId) || !string.IsNullOrEmpty(command.ProfessionalCard))
        {
            oldCollaborator.ChangedBy = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Role.Name : oldCollaborator.ChangedBy;
            oldCollaborator.EmailChangedBy = command.EmailChangeBy;
            oldCollaborator.EditionDate = editionDate;
            _collaboratorRepository.Update(oldCollaborator);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        UpdateCollaboratorResponse response = new(
            command.ProfessionalAdviceId,
            command.EducationalLevelId,
            command.ProfessionalCard,
            editionDate
            );

        return response;
    }
}