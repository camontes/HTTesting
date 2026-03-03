using ErrorOr;
using HR_Platform.Domain.BirthdayTemplateSettings;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.BirthdayTemplateSettings.Update;

internal sealed class UpdateBirthdayTemplateSettingsCommandHandler : IRequestHandler<UpdateBirthdayTemplateSettingsCommand, ErrorOr<bool>>
{
    private readonly IBirthdayTemplateSettingRepository _birthdayTemplateSettingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBirthdayTemplateSettingsCommandHandler(IBirthdayTemplateSettingRepository birthdayTemplateSettingRepository, IUnitOfWork unitOfWork)
    {
        _birthdayTemplateSettingRepository = birthdayTemplateSettingRepository ?? throw new ArgumentNullException(nameof(birthdayTemplateSettingRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<bool>> Handle(UpdateBirthdayTemplateSettingsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("BirthdayTemplateSettings.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(creationDateString) is not TimeDate editionDate)
            return Error.Validation("BirthdayTemplateSettings.EditionDate", "EditionDate is not valid");

        BirthdayTemplateSetting? birthdayTemplateSetting = await _birthdayTemplateSettingRepository.GetByCompanyIdAsync(new CompanyId(command.CompanyId));

        if (birthdayTemplateSetting is null)
        {
            BirthdayTemplateSetting temp = new(
                new BirthdayTemplateSettingId(Guid.NewGuid()),
                new CompanyId(command.CompanyId),
                command.IsDefaultTemplate, //IsDefaultTemplate,
                command.IsAllowSendPost, //IsAllowSendPost,
                command.CustomMessage is not null ? command.CustomMessage : string.Empty,
                command.CustomTemplateURL is not null ? command.CustomTemplateURL : string.Empty,
                command.CustomTemplateName is not null ? command.CustomTemplateName : string.Empty,
                true, //IsEditable
                true, //IsDeletable
                creationDate,
                editionDate
            );
            _birthdayTemplateSettingRepository.Add(temp);
        }
        else
        {
            birthdayTemplateSetting.IsDefaultTemplate = command.IsDefaultTemplate;
            birthdayTemplateSetting.IsAllowSendPost = command.IsAllowSendPost;
            birthdayTemplateSetting.CustomMessage = command.CustomMessage is not null ? command.CustomMessage : string.Empty;
            birthdayTemplateSetting.CustomTemplateURL = command.CustomTemplateURL is not null ? command.CustomTemplateURL : string.Empty;
            birthdayTemplateSetting.CustomTemplateName = command.CustomTemplateName is not null ? command.CustomTemplateName : string.Empty;
            birthdayTemplateSetting.EditionDate = editionDate;
            _birthdayTemplateSettingRepository.Update(birthdayTemplateSetting);
        }

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return Error.Validation("Update Template option dont work");
        }

    }
}