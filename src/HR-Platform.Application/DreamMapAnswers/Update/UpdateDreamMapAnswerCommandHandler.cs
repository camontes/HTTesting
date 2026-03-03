using ErrorOr;
using HR_Platform.Domain.CollaboratorDreamMapAnswers;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DreamMapAnswers;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.DreamMapAnswers.Update;

internal sealed class UpdateDreamMapAnswersCommandHandler(
    IDreamMapAnswerRepository dreamMapAnswerRepository,
    ICollaboratorDreamMapAnswerRepository collaboratorDreamMapAnswerRepository,
    ICollaboratorRepository collaboratorRepository,
    IUnitOfWork unitOfWork

    ) : IRequestHandler<UpdateDreamMapAnswersCommand, ErrorOr<bool>>
{
    private readonly IDreamMapAnswerRepository _dreamMapAnswerRepository = dreamMapAnswerRepository ?? throw new ArgumentNullException(nameof(dreamMapAnswerRepository));
    private readonly ICollaboratorDreamMapAnswerRepository _collaboratorDreamMapAnswerRepository = collaboratorDreamMapAnswerRepository ?? throw new ArgumentNullException(nameof(collaboratorDreamMapAnswerRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(UpdateDreamMapAnswersCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _collaboratorRepository.GetByEmailAsync(command.CollaboratorEmail) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide emial was not found.");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("DreamMapAnswers.EditionDate", "EditionDate is not valid");

        if (command.TemplateIndicator < 1 && command.TemplateIndicator > 4)
            return Error.Validation("DreamMapAnswers.TemplateIndicator", "Template Indicator must be between 1 and 4");

        CollaboratorDreamMapAnswer? dreamMapAnswers = await _collaboratorDreamMapAnswerRepository.GetByCollaboratorIdAsync(oldCollaborator.Id);
        List<DreamMapAnswer> dreamMapAnswerByCollaborator = [];
        bool getInto = false;
        if (dreamMapAnswers is not null)
        {
            List<DreamMapAnswer> dreamMapAnswersToAdd = [];
            dreamMapAnswers.TemplateIndicator = command.TemplateIndicator;
            dreamMapAnswers.EditionDate = editionDate;
            _collaboratorDreamMapAnswerRepository.Update(dreamMapAnswers);

            List<DreamMapAnswer> answers = await _dreamMapAnswerRepository.GetAllCollaboratorsAnswers(dreamMapAnswers.Id);

            if (answers.Count > 0)
            {
                foreach (DreamMapAnswer item in answers)
                {
                    var hasFoundAnswer = command.DreamMapAnswersDataList.Find(x => x.AnsewerId == item.Id.Value);
                    if (hasFoundAnswer is not null)
                    {
                        item.Answer = hasFoundAnswer.Answer;
                        item.EditionDate = editionDate;
                        dreamMapAnswerByCollaborator.Add(item);
                    }
                }
                _dreamMapAnswerRepository.UpdateRange(dreamMapAnswerByCollaborator);
            }
            getInto = true;
        }

        try
        {
            if (getInto)
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}