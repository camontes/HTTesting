using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.SearchFilters.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.SearchFilters;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Collaborators.CollaboratorSearchFilter;

internal sealed class CollaboratorSearchFilterQueryHandler(
    ICollaboratorRepository collaboratorRepository,

    IStringService stringService,
    ITimeFormatService timeFormatService
    ) : IRequestHandler<CollaboratorSearchFilterQuery, ErrorOr<SearchFilterResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<SearchFilterResponse>> Handle(CollaboratorSearchFilterQuery query, CancellationToken cancellationToken)
    {
        var results = await _collaboratorRepository.SearchAsync(new BasicRequestSearch { Query = query.Query, Page = query.Page, PageSize = query.PageSize, IsPendingInvitation = query.IsPendingInvitation });

        List<CollaboratorsResponse> items = [];
        if (results.TotalCount > 0)
        {
            foreach (Collaborator collaborator in results.Items)
            {
                CollaboratorsResponse temp = new
                (
                    collaborator.Id.Value,
                    collaborator.CompanyId.Value,
                    collaborator.RoleId.Value,

                    collaborator.DocumentTypeId.Value,
                    collaborator.OtherDocumentType,

                    collaborator.AssignationId.Value,

                    collaborator.CollaboratorStateId.Value,

                    collaborator.Document,

                    collaborator.DocumentType is not null ? collaborator.DocumentType.Name : string.Empty,
                    collaborator.DocumentType is not null ? collaborator.DocumentType.NameEnglish : string.Empty,

                    collaborator.Assignation is not null ? collaborator.Assignation.Name : string.Empty,
                    collaborator.Assignation is not null ? collaborator.Assignation.NameEnglish : string.Empty,

                    collaborator.BusinessEmail is not null ? collaborator.BusinessEmail.Value : string.Empty,
                    collaborator.PersonalEmail is not null ? collaborator.PersonalEmail.Value : string.Empty,

                    collaborator.Name,
                    _stringService.GetInitials(collaborator.Name),

                    collaborator.Role is not null ? collaborator.Role.Name : string.Empty,
                    collaborator.Role is not null ? collaborator.Role.NameEnglish : string.Empty,

                    !string.IsNullOrEmpty(collaborator.Photo) ? collaborator.Photo : string.Empty,
                    !string.IsNullOrEmpty(collaborator.PhotoName) ? collaborator.PhotoName : string.Empty,

                    !string.IsNullOrEmpty(collaborator.PhoneNumber) ? collaborator.PhoneNumber : string.Empty,

                    collaborator.IsSuspended,
                    collaborator.ShowNewFeatures,

                    collaborator.SuspensionReason,

                    collaborator.EntranceDate.Value,
                    _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "dd MMM yyyy", new CultureInfo("es-CO")),
                    _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "MMM dd yyyy", new CultureInfo("en-US")),

                    _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "dd/MMMM/yyyy", new CultureInfo("es-CO")),
                    _timeFormatService.GetDateFormatMonthShort(collaborator.EntranceDate.Value, "MMMM/dd/yyyy", new CultureInfo("en-US")),

                    collaborator.EditionDate.Value,
                    _timeFormatService.GetDateFormatMonthLarge(collaborator.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")),
                    _timeFormatService.GetDateFormatMonthLarge(collaborator.EditionDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")),

                    _timeFormatService.GetDateTimeFormatMonthToltip(collaborator.EditionDate.Value, "dd MMM yyyy,  HH:mm:ss tt", new CultureInfo("es-CO")),
                    _timeFormatService.GetDateTimeFormatMonthToltip(collaborator.EditionDate.Value, "MMM dd yyyy,  HH:mm:ss tt", new CultureInfo("en-US"))
                );
                items.Add(temp);
            }
        }
        return new SearchFilterResponse
        (
            results.TotalCount,
            items,
            results.TotalCount > 0 ? "Resultados encontrados." : "No se encontraron resultados."
        );
    }
}