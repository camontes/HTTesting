using ErrorOr;
using HR_Platform.Domain.CollaboratorDreamMapAnswers;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.DreamMapQuestions;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.DreamMapQuestions.Create;

internal sealed class CreateDreamMapQuestionsCommandHandler(IDreamMapQuestionRepository DreamMapQuestionRepository, ICollaboratorDreamMapAnswerRepository collaboratorDreamMapAnswerRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateDreamMapQuestionsCommand, ErrorOr<bool>>
{
    private readonly IDreamMapQuestionRepository _DreamMapQuestionRepository = DreamMapQuestionRepository ?? throw new ArgumentNullException(nameof(DreamMapQuestionRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly ICollaboratorDreamMapAnswerRepository _collaboratorDreamMapAnswerRepository = collaboratorDreamMapAnswerRepository ?? throw new ArgumentNullException(nameof(collaboratorDreamMapAnswerRepository));

    public async Task<ErrorOr<bool>> Handle(CreateDreamMapQuestionsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("TalentPools.CreationDate", "CreationDate is not valid");

        if (command.DreamMapQuestionsDataList.Count > 10)
            return Error.Validation("DreamMapQuestionsDataList.Count", "Dream Map Questions List exceeds the limit (10)");

        List<DreamMapQuestion> dreamMapQuestionsToAdd = [];

        foreach (DreamMapQuestionData dreamMapQuestionData in command.DreamMapQuestionsDataList)
        {
            DreamMapQuestion dreamMapQuestion = new(
                new DreamMapQuestionId(Guid.NewGuid()),
                new CompanyId(command.CompanyId),
                dreamMapQuestionData.Question,
                true,
                true,
                creationDate,
                creationDate
            );
            dreamMapQuestionsToAdd.Add(dreamMapQuestion);
        }

        if (dreamMapQuestionsToAdd.Count > 0)
        {
            List<CollaboratorDreamMapAnswer> dreamMapAnswers = await _collaboratorDreamMapAnswerRepository.GetAll();

            if (dreamMapAnswers is not null && dreamMapAnswers.Count > 0)
            {
                foreach (CollaboratorDreamMapAnswer map in dreamMapAnswers)
                {
                    map.SaveCurrent = false;
                }
                _collaboratorDreamMapAnswerRepository.UpdateRange(dreamMapAnswers);
            }
        }

        try
        {
            _DreamMapQuestionRepository.AddRangeDreamMapQuestions(dreamMapQuestionsToAdd);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}