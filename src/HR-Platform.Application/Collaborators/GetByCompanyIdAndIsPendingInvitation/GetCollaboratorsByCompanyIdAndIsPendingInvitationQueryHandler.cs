using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

internal sealed class GetCollaboratorsByCompanyIdAndIsPendingInvitationQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    ICompanyRepository companyRepository,

    IStringService stringService,
    ITimeFormatService timeFormatService
    ) : IRequestHandler<GetCollaboratorsByCompanyIdAndIsPendingInvitationQuery, ErrorOr<CollaboratorsAndCountResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<CollaboratorsAndCountResponse>> Handle(GetCollaboratorsByCompanyIdAndIsPendingInvitationQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }

        if (await _collaboratorRepository.GetByCompanyIdAndIsPendingInvitation(
            (new CompanyId(query.CompanyId)), query.IsPendingInvitation, query.Page, query.PageSize) is not List<Collaborator> collaborators)
        {
            return Error.NotFound("Assignation.NotFound", "The collaborators related with the provide Id and assgnation type was not found.");
        }

        int collaboratorsCount = await _collaboratorRepository.GetCountByCompanyIdAndIsPendingInvitation(
            (new CompanyId(query.CompanyId)), query.IsPendingInvitation);

        List<CollaboratorsResponse> collaboratorsResponse = [];

        if (collaborators is not null && collaborators.Count > 0)
        {
            foreach (Collaborator collaborator in collaborators)
            {
                collaboratorsResponse.Add
                (
                    new CollaboratorsResponse
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
                        collaborator.Assignation  is not null ? collaborator.Assignation.NameEnglish : string.Empty,

                        collaborator.BusinessEmail is not null ? collaborator.BusinessEmail.Value : string.Empty,
                        collaborator.PersonalEmail is not null && collaborator.PersonalEmail.Value is not null ? collaborator.PersonalEmail.Value : string.Empty,

                        collaborator.Name,
                        _stringService.GetInitials(collaborator.Name),

                        //collaborator.PhoneNumber.Value,
                        //collaborator.CellphoneNumber is not null && collaborator.CellphoneNumber.Value is not null ? collaborator.CellphoneNumber.Value : string.Empty,

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

                        _timeFormatService.GetDateTimeFormatMonthToltip(collaborator.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")),
                        _timeFormatService.GetDateTimeFormatMonthToltip(collaborator.EditionDate.Value, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US"))
                    )
                );
            }
        }

        CollaboratorsAndCountResponse collaboratorsAndCount = new
        (
            [.. collaboratorsResponse.OrderByDescending(x => x.EntranceDate)],
            collaboratorsCount // Without SuperAdmin
        );

        return collaboratorsAndCount;
    }
}
