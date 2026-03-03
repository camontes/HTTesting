using ErrorOr;
using HR_Platform.Application.BirthdayTemplateSettings.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.BirthdayTemplateSettings;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.BirthdayTemplateSettings.GetByCompanyId;

internal sealed class CollaboratorQueryHandler(
    IBirthdayTemplateSettingRepository birthdayTemplateSettingRepository,
    ICollaboratorRepository collaboratorRepository,
    ITimeFormatService timeFormatService

    ) : IRequestHandler<GetCollaboratorsQuery, ErrorOr<BirthdayCollaboratorsResponse>>
{
    private readonly IBirthdayTemplateSettingRepository _birthdayTemplateSettingRepository = birthdayTemplateSettingRepository ?? throw new ArgumentNullException(nameof(birthdayTemplateSettingRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<BirthdayCollaboratorsResponse>> Handle(GetCollaboratorsQuery query, CancellationToken cancellationToken)
    {
        List<Collaborator> collaboratorList = await _collaboratorRepository.GetAllByCompanyId(new CompanyId(query.CompanyId));
        BirthdayTemplateSetting? birthdayTemplateSetting = await _birthdayTemplateSettingRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId));
        BirthdayTemplateSettingsResponse templateSettings;
        List<CollaboratorBirthdayInfo> collaborators = [];

        if (birthdayTemplateSetting is null)
        {
            templateSettings = new BirthdayTemplateSettingsResponse
            (
                true, // IsDefaultTemplate,
                true, // IsAllowSendPost,
                "",// CustomMessage,
                "",// CustomTemplateURL,
                ""// CustomTemplateName
            );
        }
        else
        {
            templateSettings = new BirthdayTemplateSettingsResponse
             (
                 birthdayTemplateSetting.IsDefaultTemplate, // IsDefaultTemplate,
                 birthdayTemplateSetting.IsAllowSendPost, // IsAllowSendPost,
                 birthdayTemplateSetting.CustomMessage,
                 birthdayTemplateSetting.CustomTemplateURL,
                 birthdayTemplateSetting.CustomTemplateName
             );
        }

        if (collaboratorList is not null && collaboratorList.Count > 0)
        {
            DateTime today = DateTime.Today;
            List<Collaborator> collaboratorListByBirhday = collaboratorList.Where(e => e.Birthdate != null &&
                    e.Birthdate.Value.Month == today.Month &&
                    e.Birthdate.Value.Day <= today.Day)
                    .ToList();

            if (collaboratorListByBirhday is not null && collaboratorListByBirhday.Count > 0)
            {
                foreach (Collaborator item in collaboratorListByBirhday)
                {
                    if (item.Birthdate.Value != item.CreationDate.Value)
                    {
                        CollaboratorBirthdayInfo birthdate = new
                        (
                            item.Name,
                            item.Photo,
                            _timeFormatService.GetDateFormatMonthLarge(item.Birthdate.Value, "dd MMMM", new CultureInfo("es-CO"))
                                .Substring(0, _timeFormatService.GetDateFormatMonthLarge(item.Birthdate.Value, "dd MMMM", new CultureInfo("es-CO")).Length - 5), // UpdatedFormat,
                            _timeFormatService.GetDateFormatMonthLarge(item.Birthdate.Value, "MMMM dd", new CultureInfo("en-US")) // UpdatedFormatEnglish

                        );

                        collaborators.Add(birthdate);
                    }
                }
            }
        }

        BirthdayCollaboratorsResponse result = new
        (
            templateSettings.IsDefaultTemplate, // IsDefaultTemplate,
            templateSettings.IsAllowSendPost, // IsAllowSendPost,
            templateSettings.CustomMessage, 
            templateSettings.CustomTemplateURL,
            templateSettings.CustomTemplateName,
            collaborators
        );

        return result;
    }
}