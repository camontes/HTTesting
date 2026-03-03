using ErrorOr;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.FormAnswerGroups;
using HR_Platform.Domain.FormAnswerGroupStates;
using HR_Platform.Domain.FormAnswers;
using HR_Platform.Domain.FormQuestionsTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Forms.CreateAnswerByFormId;

internal sealed class CreateAnswerByFormIdCommandHandler(
    IFormQuestionsTypeRepository formQuestionsTypeRepository,
    ICollaboratorRepository collaboratorRepository,
    IAreaRepository areaRepository,
    IFormAnswerRepository formAnswerRepository,
    IFormAnswerGroupRepository formAnswerGroupRepository,
    IReferenceGenerator referenceGenerator,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateAnswerByFormIdCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IFormQuestionsTypeRepository _formQuestionsTypeRepository = formQuestionsTypeRepository ?? throw new ArgumentNullException(nameof(formQuestionsTypeRepository));
    private readonly IReferenceGenerator _referenceGenerator = referenceGenerator ?? throw new ArgumentNullException(nameof(referenceGenerator));
    private readonly IAreaRepository _areaRepository = areaRepository ?? throw new ArgumentNullException(nameof(areaRepository));
    private readonly IFormAnswerRepository _formAnswerRepository = formAnswerRepository ?? throw new ArgumentNullException(nameof(formAnswerRepository));
    private readonly IFormAnswerGroupRepository _formAnswerGroupRepository = formAnswerGroupRepository ?? throw new ArgumentNullException(nameof(formAnswerGroupRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateAnswerByFormIdCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Form.CreationDate", "CreationDate is not valid");


        Collaborator? CollaboratorWhoIsLogin = await _collaboratorRepository.GetByEmailAsync(command.EmailWhoIsLogin);

        if (CollaboratorWhoIsLogin is null)
            return Error.Validation("Form.Validation", "The Collaborator With the provide Email was not found.");


        if (await _areaRepository.GetByIdAsync(new AreaId(command.NoveltyTypeId)) is not Area NoveltyType)
            return Error.Validation("Form.NoveltyTypeId", "The Novelty Type with the provide Id was not found.");

        string areaAssined = NoveltyType.NameEnglish switch
        {
            "Human Talent" => "TH",
            "Operations" => "OP",
            "Infrastructure" => "IF",
            _ => ""
        };

        string startReference = $"NOV_{areaAssined}";
        string reference = _referenceGenerator.GenerateReference(startReference);

        List<FormAnswer> formAnswers = [];

        if (command.AnswerObjects is not null && command.AnswerObjects.Count > 0)
        {
            if (await _formQuestionsTypeRepository.GetByIdAsync(new FormQuestionsTypeId(command.AnswerObjects[0].FormQuestionsTypeId)) 
                is not FormQuestionsType auxFormQuestionsType)
                return Error.NotFound("FormAsnwer.NotFound", "The Form Questions Type with the provide Id was not found.");

            FormAnswerGroupId formAnswerGroupId = new(Guid.NewGuid());

            FormAnswerGroup formAnswerGroup = new
            (
                formAnswerGroupId,

                auxFormQuestionsType.FormId,

                CollaboratorWhoIsLogin.Id,

                new FormAnswerGroupStateId(1),

                reference,

                string.Empty, // DescriptionState

                string.Empty, // File
                string.Empty, // FileName

                true, // IsEditable,
                true, // IsDeleteable,

                creationDate,
                creationDate
            );

            _formAnswerGroupRepository.Add(formAnswerGroup);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            foreach (AnswerObject answer in command.AnswerObjects)
            {
                //Only For Short Text
                if (answer.Answer.Length > 500)
                    return Error.Validation("FormAsnwer.Validation", "The answer exceeds the 500 character limit.");

                if (await _formQuestionsTypeRepository.GetByIdAsync(new FormQuestionsTypeId(answer.FormQuestionsTypeId)) is not FormQuestionsType oldFormQuestionsType)
                    return Error.NotFound("FormAsnwer.NotFound", "The Form Questions Type with the provide Id was not found.");

                FormAnswer formAnswer = new
                (
                    new FormAnswerId(Guid.NewGuid()),

                    oldFormQuestionsType.Id,

                    CollaboratorWhoIsLogin.Id,

                    formAnswerGroupId,

                    answer.Answer,

                    reference,

                    true, //IsEditable
                    true, //IsDeletable

                    creationDate,
                    creationDate
                );

                formAnswers.Add(formAnswer);
            }
        }

        _formAnswerRepository.AddRange(formAnswers);

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