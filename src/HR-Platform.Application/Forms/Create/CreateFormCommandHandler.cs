using ErrorOr;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.FormQuestionsTypes;
using HR_Platform.Domain.Forms;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.QuestionTypes;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Forms.Create;

internal sealed class CreateFormsCommandHandler(
    IFormQuestionsTypeRepository formQuestionsTypeRepository,
    IAreaRepository areaRepository,
    IFormRepository formRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateFormCommand, ErrorOr<bool>>
{
    private readonly IFormQuestionsTypeRepository _formQuestionsTypeRepository = formQuestionsTypeRepository ?? throw new ArgumentNullException(nameof(formQuestionsTypeRepository));
    private readonly IFormRepository _formRepository = formRepository ?? throw new ArgumentNullException(nameof(formRepository));
    private readonly IAreaRepository _areaRepository = areaRepository ?? throw new ArgumentNullException(nameof(areaRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateFormCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Form.CreationDate", "CreationDate is not valid");

        if (await _areaRepository.GetByIdAsync(new AreaId(command.NoveltyTypeId)) is not Area NoveltyType)
            return Error.Validation("Form.NoveltyTypeId", "The Novelty Type with the provide Id was not found.");

        Form form = new
        (
            new FormId(Guid.NewGuid()),
            new CompanyId(command.CompanyId),

            command.Name,
            command.Description,

            new AreaId(command.NoveltyTypeId),

            false, // IsVisible

            true, //IsEditable
            true, //IsDeletable

            creationDate,
            creationDate
        );

        List<FormQuestionsType> formQuestionsType = command.QuestionTypeRequests.Select(q => new FormQuestionsType
           (
                new FormQuestionsTypeId(Guid.NewGuid()),
                form.Id,
                new QuestionTypeId(q.QuestionTypeId),
                q.Question,
                q.IsRequired,
                true, //IsEditable
                true, //IsDeletable
                creationDate,
                creationDate

           )).ToList();

        _formRepository.Add(form);
        _formQuestionsTypeRepository.AddRange(formQuestionsType);

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