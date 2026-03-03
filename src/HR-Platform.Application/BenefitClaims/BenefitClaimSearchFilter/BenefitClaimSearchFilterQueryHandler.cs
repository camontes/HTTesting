using ErrorOr;
using HR_Platform.Application.BenefitClaims.Common;
using HR_Platform.Application.SearchFilters.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorBenefitClaims;
using HR_Platform.Domain.SearchFilters;
using MediatR;
using System.Globalization;

namespace BenefitClaims.BenefitClaimSearchFilter;

internal sealed class BenefitClaimSearchFilterQueryHandler(
        ICollaboratorBenefitClaimRepository collaboratorBenefitClaimRepository,

    ITimeFormatService timeFormatService
    ) : IRequestHandler<BenefitClaimSearchFilterQuery, ErrorOr<SearchFilterResponse>>
{
    private readonly ICollaboratorBenefitClaimRepository _collaboratorBenefitClaimRepository = collaboratorBenefitClaimRepository ?? throw new ArgumentNullException(nameof(collaboratorBenefitClaimRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<SearchFilterResponse>> Handle(BenefitClaimSearchFilterQuery query, CancellationToken cancellationToken)
    {
        var results = await _collaboratorBenefitClaimRepository.SearchAsync(new BasicRequestSearch { Query = query.Query, Page = query.Page, PageSize = query.PageSize });

        List<BenefitClaimsResponse> items = [];

        if (results.TotalCount > 0)
        {
            foreach (CollaboratorBenefitClaim item in results.Items)
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
                    _timeFormatService.GetDateFormatMonthLarge(item.Collaborator.EntranceDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")), // ClaimDateEnglish
                    _timeFormatService.GetDateFormatMonthLarge(item.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // ClaimDate,
                    _timeFormatService.GetDateFormatMonthLarge(item.EditionDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")), // ClaimDateEnglish
                    _timeFormatService.GetDateTimeFormatMonthToltip(item.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), //UpdateToltip
                    item.Benefit.Name,
                    item.EditionDate.Value
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