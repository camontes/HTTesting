using ErrorOr;
using HR_Platform.Domain.CollaboratorDreamMapAnswers;
using HR_Platform.Domain.DreamMapQuestions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.DreamMapQuestions.Update;

internal sealed class UpdateDreamMapQuestionCommandHandler(
    IDreamMapQuestionRepository dreamMapQuestionRepository,
    ICollaboratorDreamMapAnswerRepository collaboratorDreamMapAnswerRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateDreamMapQuestionsCommand, ErrorOr<bool>>
{
    private readonly IDreamMapQuestionRepository _dreamMapQuestionRepository = dreamMapQuestionRepository ?? throw new ArgumentNullException(nameof(dreamMapQuestionRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly ICollaboratorDreamMapAnswerRepository _collaboratorDreamMapAnswerRepository = collaboratorDreamMapAnswerRepository ?? throw new ArgumentNullException(nameof(collaboratorDreamMapAnswerRepository));

    public async Task<ErrorOr<bool>> Handle(UpdateDreamMapQuestionsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("TalentPools.EditionDate", "EditionDate is not valid");

        if (command.DreamMapQuestionsDataList.Count > 10)
            return Error.Validation("DreamMapQuestionsDataList.Count", "Dream Map Questions List exceeds the limit (10)");

        List<DreamMapQuestion> questions = await _dreamMapQuestionRepository.GetAll();

        List<DreamMapQuestion> dreamMapQuestionsToUpdate = [];
        List<DreamMapQuestion> dreamMapQuestionsToDelete = [];
        if (questions.Count > 0)
        {

            foreach (DreamMapQuestion item in questions)
            {
                var hasFoundQuestion = command.DreamMapQuestionsDataList.Find(x => x.QuestionId == item.Id.Value);
                if (hasFoundQuestion is not null)
                {
                    item.Question = hasFoundQuestion.Question;
                    item.EditionDate = editionDate;
                    dreamMapQuestionsToUpdate.Add(item);
                }
                else
                {
                    dreamMapQuestionsToDelete.Add(item);
                }
            }
        }

        if (dreamMapQuestionsToUpdate.Count > 0)
        {
            _dreamMapQuestionRepository.UpdateRange(dreamMapQuestionsToUpdate);
        }

        if (dreamMapQuestionsToDelete.Count > 0)
        {
            _dreamMapQuestionRepository.DeleteRange(dreamMapQuestionsToDelete);
        }

        List<CollaboratorDreamMapAnswer> dreamMapAnswers = await _collaboratorDreamMapAnswerRepository.GetAll();

        if (dreamMapAnswers is not null && dreamMapAnswers.Count > 0)
        {
            foreach (CollaboratorDreamMapAnswer map in dreamMapAnswers)
            {
                map.SaveCurrent = false;
            }
            _collaboratorDreamMapAnswerRepository.UpdateRange(dreamMapAnswers);
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