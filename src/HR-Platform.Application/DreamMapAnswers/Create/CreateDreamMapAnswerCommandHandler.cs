using ErrorOr;
using HR_Platform.Domain.CollaboratorDreamMapAnswers;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DreamMapAnswers;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.DreamMapAnswers.Create;

internal sealed class CreateDreamMapAnswersCommandHandler(
    IDreamMapAnswerRepository dreamMapAnswerRepository,
    ICollaboratorDreamMapAnswerRepository collaboratorDreamMapAnswerRepository,
    ICollaboratorRepository collaboratorRepository,
    IUnitOfWork unitOfWork

    ) : IRequestHandler<CreateDreamMapAnswersCommand, ErrorOr<bool>>
{
    private readonly IDreamMapAnswerRepository _dreamMapAnswerRepository = dreamMapAnswerRepository ?? throw new ArgumentNullException(nameof(dreamMapAnswerRepository));
    private readonly ICollaboratorDreamMapAnswerRepository _collaboratorDreamMapAnswerRepository = collaboratorDreamMapAnswerRepository ?? throw new ArgumentNullException(nameof(collaboratorDreamMapAnswerRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateDreamMapAnswersCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _collaboratorRepository.GetByEmailAsync(command.CollaboratorEmail) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide emial was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("TalentPools.CreationDate", "CreationDate is not valid");

        if (command.TemplateIndicator < 1 && command.TemplateIndicator > 4)
            return Error.Validation("TalentPools.TemplateIndicator", "Template Indicator must be between 1 and 4");

        List<DreamMapAnswer> dreamMapAnswersToAdd = [];

        CollaboratorDreamMapAnswer generalSave = new
        (
            new CollaboratorDreamMapAnswerId(Guid.NewGuid()),
            oldCollaborator.Id,
            command.TemplateIndicator,
            true, //SaveCurrent - if it is true save the current dream map & if it is false appear on the screen
            true, //IsEditable
            true, //IsDeletable
            creationDate,
            creationDate
        );

        _collaboratorDreamMapAnswerRepository.Add(generalSave);


        foreach (DreamMapAnswerData dreamMapAnswerData in command.DreamMapAnswersDataList)
        {
            DreamMapAnswer dreamMapAnswer = new(
                new DreamMapAnswerId(Guid.NewGuid()),
                generalSave.Id,
                dreamMapAnswerData.Question,
                dreamMapAnswerData.Answer,
                true, //IsEditable
                true, //IsDeletable
                creationDate,
                creationDate
            );
            dreamMapAnswersToAdd.Add(dreamMapAnswer);
        }

        _dreamMapAnswerRepository.AddRangeDreamMapAnswers(dreamMapAnswersToAdd);

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