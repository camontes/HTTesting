using ErrorOr;
using HR_Platform.Application.BenefitClaims.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorBenefitClaims;
using HR_Platform.Domain.Companies;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.BenefitClaims.GetAllClaimsByCompanyId;


internal sealed class GetAllClaimsByCompanyIdQueryHandler(
    ICollaboratorBenefitClaimRepository collaboratorBenefitClaimRepository,
    ITimeFormatService timeFormatService

    ) : IRequestHandler<GetAllClaimsByCompanyIdQuery, ErrorOr<List<BenefitClaimsResponse>>>
{
    private readonly ICollaboratorBenefitClaimRepository _collaboratorBenefitClaimRepository = collaboratorBenefitClaimRepository ?? throw new ArgumentNullException(nameof(collaboratorBenefitClaimRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<List<BenefitClaimsResponse>>> Handle(GetAllClaimsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        List<CollaboratorBenefitClaim>? collaboratorBenefitClaims =  await _collaboratorBenefitClaimRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId));
        List<BenefitClaimsResponse> benefitClaimsResult = [];

        if (collaboratorBenefitClaims is not null && collaboratorBenefitClaims.Count > 0)
        {
            foreach (CollaboratorBenefitClaim item in collaboratorBenefitClaims)
            {
                BenefitClaimsResponse temp = new
                (
                    item.Id.Value,
                    item.Collaborator.Id.Value,
                    item.Collaborator.DocumentType is not null ? item.Collaborator.DocumentType.Name : string.Empty,
                    item.Collaborator.DocumentType is not null ? item.Collaborator.OtherDocumentType : string.Empty,
                    item.Collaborator.Document,
                    item.Collaborator.Name,
                    item.Collaborator.Assignation.Name,
                    _timeFormatService.GetDateFormatMonthLarge(item.Collaborator.EntranceDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // ClaimDate,
                    _timeFormatService.GetDateFormatMonthLarge(item.Collaborator    .EntranceDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")), // ClaimDateEnglish
                    _timeFormatService.GetDateFormatMonthLarge(item.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // ClaimDate,
                    _timeFormatService.GetDateFormatMonthLarge(item.EditionDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")), // ClaimDateEnglish
                    _timeFormatService.GetDateTimeFormatMonthToltip(item.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), //UpdateToltip
                    item.Benefit.Name,
                    item.EditionDate.Value
                );
                benefitClaimsResult.Add(temp);
            }
        }
        return benefitClaimsResult.OrderBy(x => x.EditionDate).ToList();
    }
}