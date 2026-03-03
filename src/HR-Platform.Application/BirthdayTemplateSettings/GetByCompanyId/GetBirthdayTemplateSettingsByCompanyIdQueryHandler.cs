using ErrorOr;
using HR_Platform.Application.BirthdayTemplateSettings.Common;
using HR_Platform.Domain.BirthdayTemplateSettings;
using HR_Platform.Domain.Companies;
using MediatR;

namespace HR_Platform.Application.BirthdayTemplateSettings.GetByCompanyId;

internal sealed class GetBirthdayTemplateSettingsByCompanyIdHandler(
    IBirthdayTemplateSettingRepository birthdayTemplateSettingRepository
    ) : IRequestHandler<GetBirthdayTemplateSettingsByCompanyIdQuery, ErrorOr<BirthdayTemplateSettingsResponse>>
{
    private readonly IBirthdayTemplateSettingRepository _birthdayTemplateSettingRepository = birthdayTemplateSettingRepository ?? throw new ArgumentNullException(nameof(birthdayTemplateSettingRepository));

    public async Task<ErrorOr<BirthdayTemplateSettingsResponse>> Handle(GetBirthdayTemplateSettingsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        BirthdayTemplateSetting? birthdayTemplateSetting = await _birthdayTemplateSettingRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId));
        BirthdayTemplateSettingsResponse result;
        if (birthdayTemplateSetting is null)
        {
            result = new BirthdayTemplateSettingsResponse
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
            result = new BirthdayTemplateSettingsResponse
             (
                 birthdayTemplateSetting.IsDefaultTemplate, // IsDefaultTemplate,
                 birthdayTemplateSetting.IsAllowSendPost, // IsAllowSendPost,
                 birthdayTemplateSetting.CustomMessage,
                 birthdayTemplateSetting.CustomTemplateURL,
                 birthdayTemplateSetting.CustomTemplateName
             );
        }
        return result;
    }
}