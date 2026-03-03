using ErrorOr;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using HR_Platform.Domain.SurveyQuestions;
using HR_Platform.Domain.Surveys;
using HR_Platform.Domain.Areas;
using HR_Platform.Domain.SurveyQuestionTypes;
using HR_Platform.Domain.SurveyQuestionMandatoryTypes;

namespace HR_Platform.Application.Surveys.Create;

internal sealed class CreateSurveyCommandHandler
(
    ICollaboratorRepository collaboratorRepository,
    ICompanyRepository companyRepository,
    ISurveyRepository surveyRepository,
    ISurveyQuestionRepository surveyQuestionRepository,

    IUnitOfWork unitOfWork
)
:
IRequestHandler<CreateSurveyCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly ISurveyRepository _surveyRepository = surveyRepository ?? throw new ArgumentNullException(nameof(surveyRepository));
    private readonly ISurveyQuestionRepository _surveyQuestionRepository = surveyQuestionRepository ?? throw new ArgumentNullException(nameof(surveyQuestionRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateSurveyCommand command, CancellationToken cancellationToken)
    {
        DateTime colombianTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = colombianTime.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _companyRepository.GetByIdAsync(new CompanyId(command.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Surveys.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailWhoChangedByTH);

        Survey survey = new
        (
            new SurveyId(Guid.NewGuid()),

            new CompanyId(command.CompanyId),

            new AreaId(command.SurveyTypeId),

            command.Name,

            command.Description,

            command.EmailWhoChangedByTH,
            command.NameWhoChangedByTH,

            command.IsVisible,

            true, // IsEditable
            true, // IsDeleteable

            creationDate,
            creationDate
        );

        _surveyRepository.Add(survey);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        List<SurveyQuestion> surveyQuestions = [];

        foreach (CreateBaseSurveyQuestionCommand surveyQuestionCommand in command.SurveyQuestions)
        {
            SurveyQuestion surveyQuestion = new
            (
                new SurveyQuestionId(Guid.NewGuid()),

                survey.Id,

                new SurveyQuestionTypeId(surveyQuestionCommand.SurveyQuestionTypeId),

                new SurveyQuestionMandatoryTypeId(surveyQuestionCommand.SurveyQuestionMandatoryTypeId),

                surveyQuestionCommand.Text,

                true, // IsEditable
                true, // IsDeleteable

                creationDate,
                creationDate
            );

            surveyQuestions.Add(surveyQuestion);
        }

        if (surveyQuestions.Count > 0)
        {
            _surveyQuestionRepository.AddRange(surveyQuestions);
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