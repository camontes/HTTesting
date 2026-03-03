using ErrorOr;
using HR_Platform.Application.BirthdayTemplateSettings.Common;
using MediatR;

namespace HR_Platform.Application.BirthdayTemplateSettings.GetByCompanyId;

public record GetBirthdayTemplateSettingsByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<BirthdayTemplateSettingsResponse>>;