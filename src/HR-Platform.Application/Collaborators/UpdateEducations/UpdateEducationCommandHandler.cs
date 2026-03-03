using ErrorOr;
using HR_Platform.Application.Collaborators.CreateEducations;
using HR_Platform.Domain.CollaboratorEducations;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DefaultEducationStages;
using HR_Platform.Domain.DefaultProfessions;
using HR_Platform.Domain.DefaultStudyAreas;
using HR_Platform.Domain.DefaultStudyTypes;
using HR_Platform.Domain.EducationalLevels;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateBasicInformation;

internal sealed class UpdateEducationCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorEducationRepository collaboratorEducationRepository,
    IDefaultProfessionRepository defaultProfessionRepository,

    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateEducationCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorEducationRepository _collaboratorEducationRepository = collaboratorEducationRepository ?? throw new ArgumentNullException(nameof(collaboratorEducationRepository));
    private readonly IDefaultProfessionRepository _defaultProfessionRepository = defaultProfessionRepository ?? throw new ArgumentNullException(nameof(defaultProfessionRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateEducationCommand command, CancellationToken cancellationToken)
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


        DefaultProfession? defaultOthorProfession = await _defaultProfessionRepository.GetOtheProfessionId();

        TimeDate? endEducationDate = null;
        if (command.EndEducationDate is not null)
        {
            string? dateString = command.EndEducationDate?.ToString("MM/dd/yyyy HH:mm:ss");

            TimeDate? tempDate = !string.IsNullOrEmpty(dateString) ? TimeDate.Create(dateString) : null;

            if (tempDate is null)
            {
                return Error.Validation("Collaborators.EndEducationDate", "EndEducationDate is not valid");
            }
            else
            {
                endEducationDate = tempDate;
            }
        }

        TimeDate? startEducationDate = null;
        if (command.StartEducationDate is not null)
        {
            string? dateString = command.StartEducationDate?.ToString("MM/dd/yyyy HH:mm:ss");

            TimeDate? tempDate = !string.IsNullOrEmpty(dateString) ? TimeDate.Create(dateString) : null;

            if (tempDate is null)
            {
                return Error.Validation("Collaborators.StartEducationDate", "StartEducationDate is not valid");
            }
            else
            {
                startEducationDate = tempDate;
            }
        }

        if (new DefaultProfessionId(command.ProfessionId) == defaultOthorProfession?.Id && string.IsNullOrEmpty(command.OtherProfessionName))
            return Error.Validation("The name of the other profession is mandatory");

        if (command.IsCompletedStudy is true && command.EndEducationDate is null)
            return Error.Validation("Missing End Date");

        if (command.IsCompletedStudy is false && command.EducationStageId is 0 && command.StartEducationDate is null)
            return Error.Validation("Missing information: It's mandatory Current State and Start Date");
        CollaboratorEducation educationInfo = new(
                new CollaboratorEducationId(Guid.NewGuid()),
                oldCollaborator.Id,
                command.InstitutionName,
                new DefaultProfessionId(command.ProfessionId),
                command.OtherProfessionName is not null ? command.OtherProfessionName : string.Empty,
                command.EducationLevelId is not null && Guid.TryParse(command.EducationLevelId, out _) ? new EducationalLevelId(Guid.Parse(command.EducationLevelId)) : null,
                command.StudyAreaId is not 0 ? new DefaultStudyTypeId(command.StudyTypeId) : null,
                command.IsCertificated, //Bool
                command.StudyAreaId is not 0 ? new DefaultStudyAreaId(command.StudyAreaId) : null,
                command.IsCompletedStudy, //Bool
                endEducationDate is not null ? endEducationDate : null, //End Date
                startEducationDate is not null ? startEducationDate : null, //Start Date
                command.EducationStageId is not 0 ? new DefaultEducationStageId(command.EducationStageId) : null, //Education State 
                command.EducationFileURL,//FilesURL
                command.EducationFileName,//FilesName
                true, //IsEditable
                true, //IsDeleteable
                editionDate, //Creation Date
                editionDate //Editaion Date
         );


        try
        {
            _collaboratorEducationRepository.Add(educationInfo);

            oldCollaborator.ChangedBy = CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Role.Name : oldCollaborator.ChangedBy;
            oldCollaborator.EmailChangedBy = command.EmailChangeBy;
            oldCollaborator.EditionDate = editionDate;
            _collaboratorRepository.Update(oldCollaborator);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}