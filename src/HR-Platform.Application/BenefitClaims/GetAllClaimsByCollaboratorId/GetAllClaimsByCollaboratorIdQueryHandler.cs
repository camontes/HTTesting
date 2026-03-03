using ErrorOr;
using HR_Platform.Application.BenefitClaims.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorBenefitClaims;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.BenefitClaims.GetAllClaimsByCompanyId;


internal sealed class GetAllClaimsByCollaboratorIdQueryHandler(
    ICollaboratorBenefitClaimRepository collaboratorBenefitClaimRepository,
    ITimeFormatService timeFormatService

    ) : IRequestHandler<GetAllClaimsByCollaboratorIdQuery, ErrorOr<CollaboratorBenefitClaimsResponse>>
{
    private readonly ICollaboratorBenefitClaimRepository _collaboratorBenefitClaimRepository = collaboratorBenefitClaimRepository ?? throw new ArgumentNullException(nameof(collaboratorBenefitClaimRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<CollaboratorBenefitClaimsResponse>> Handle(GetAllClaimsByCollaboratorIdQuery query, CancellationToken cancellationToken)
    {
        CollaboratorBenefitClaim? collaboratorBenefitClaims = await _collaboratorBenefitClaimRepository.GetByIdAsync(new CollaboratorBenefitClaimId(query.BenefitClaimId));

        CollaboratorBenefitClaimsResponse response = new
        (
            collaboratorBenefitClaims is not null ? collaboratorBenefitClaims.Collaborator.Id.Value.ToString() : string.Empty,
            collaboratorBenefitClaims is not null && collaboratorBenefitClaims.Collaborator.DocumentType is not null ? collaboratorBenefitClaims.Collaborator.DocumentType.Name : string.Empty,
            collaboratorBenefitClaims is not null && collaboratorBenefitClaims.Collaborator.DocumentType is not null ? collaboratorBenefitClaims.Collaborator.OtherDocumentType : string.Empty,
            collaboratorBenefitClaims is not null ? collaboratorBenefitClaims.Collaborator.Document : string.Empty,
            collaboratorBenefitClaims is not null ? collaboratorBenefitClaims.Collaborator.Name : string.Empty,
            collaboratorBenefitClaims is not null ? _timeFormatService.GetDateFormatMonthLarge(collaboratorBenefitClaims.Collaborator.EntranceDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")) : string.Empty,
            collaboratorBenefitClaims is not null ? _timeFormatService.GetDateFormatMonthLarge(collaboratorBenefitClaims.Collaborator.EntranceDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")) : string.Empty,
            collaboratorBenefitClaims is not null ? collaboratorBenefitClaims.ReferenceNumber : string.Empty,
            collaboratorBenefitClaims is not null && collaboratorBenefitClaims.Benefit.EditionDate.Value > collaboratorBenefitClaims.CreationDate.Value ? _timeFormatService.GetDateFormatMonthLarge(collaboratorBenefitClaims.Benefit.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")) : string.Empty, // UpdatedBenefitDate,
            collaboratorBenefitClaims is not null && collaboratorBenefitClaims.Benefit.EditionDate.Value > collaboratorBenefitClaims.CreationDate.Value ? _timeFormatService.GetDateFormatMonthLarge(collaboratorBenefitClaims.Benefit.EditionDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")): string.Empty, // UpdatedBenefitDateEnglish
            collaboratorBenefitClaims is not null && collaboratorBenefitClaims.Benefit.EditionDate.Value > collaboratorBenefitClaims.CreationDate.Value ? _timeFormatService.GetDateTimeFormatMonthToltip(collaboratorBenefitClaims.Benefit.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")) : string.Empty, //UpdatedBenefitDateToltip
            collaboratorBenefitClaims is not null ? collaboratorBenefitClaims.Benefit.Name : string.Empty,
            collaboratorBenefitClaims is not null ? _timeFormatService.GetDateFormatMonthLarge(collaboratorBenefitClaims.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")) : string.Empty, // ClaimDate,
            collaboratorBenefitClaims is not null ? _timeFormatService.GetDateFormatMonthLarge(collaboratorBenefitClaims.EditionDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")) : string.Empty, // ClaimDateEnglish
            collaboratorBenefitClaims is not null ? _timeFormatService.GetDateTimeFormatMonthToltip(collaboratorBenefitClaims.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")) : string.Empty, //UpdateToltip
            collaboratorBenefitClaims is not null && collaboratorBenefitClaims.Benefit.IsAvailableForAll,
            collaboratorBenefitClaims is not null && collaboratorBenefitClaims.Benefit.MinimumMonthsConstraint != 0 ? collaboratorBenefitClaims.Benefit.MinimumMonthsConstraint.ToString() : string.Empty,
            collaboratorBenefitClaims is not null ? collaboratorBenefitClaims.Benefit.AnotherContraint : string.Empty
        );

        return response;
    }
}