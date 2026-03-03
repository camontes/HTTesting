using ErrorOr;
using HR_Platform.Application.BenefitClaimAnswers.Common;
using HR_Platform.Application.SearchFilters.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.BenefitClaimAnswers;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.SearchFilters;
using MediatR;
using System.Globalization;

namespace BenefitClaimAnswers.BenefitClaimAnswerSearchFilter;

internal sealed class BenefitClaimAnswerSearchFilterQueryHandler(
        IBenefitClaimAnswerRepository benefitClaimAnswerRepository,
        ICollaboratorRepository collaboratorRepository,
        ITimeFormatService timeFormatService,
        IStringService stringService
    ) : IRequestHandler<BenefitClaimAnswerSearchFilterQuery, ErrorOr<SearchFilterResponse>>
{
    private readonly IBenefitClaimAnswerRepository _benefitClaimAnswerRepository = benefitClaimAnswerRepository ?? throw new ArgumentNullException(nameof(benefitClaimAnswerRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));

    public async Task<ErrorOr<SearchFilterResponse>> Handle(BenefitClaimAnswerSearchFilterQuery query, CancellationToken cancellationToken)
    {
        var results = await _benefitClaimAnswerRepository.SearchAsync(new BasicRequestSearch { Query = query.Query, Page = query.Page, PageSize = query.PageSize });

        List<ClaimSentResponse> items = [];

        if (results.TotalCount > 0)
        {
            var collaboratorBenefitClaimAnswerByCollaborator = results.Items.GroupBy(x => x.Collaborator.Id);

            foreach (var group in collaboratorBenefitClaimAnswerByCollaborator)
            {
                Collaborator tempCollaborator = group.First().Collaborator;

                List<ClaimSentHistoryResponse> claimSentHistory = [];

                foreach (var claimSentResponse in group)
                {
                    Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(claimSentResponse.EmailWhoManagedClaim);

                    ClaimSentHistoryResponse temp = new
                    (
                       claimSentResponse.Id.Value.ToString(),
                       claimSentResponse.ReferenceNumber,
                       claimSentResponse.BenefitName,
                       claimSentResponse.IsBenefitAccepeted,
                       _timeFormatService.GetDateFormatMonthLarge(claimSentResponse.ManagementDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // ClaimDate,
                       _timeFormatService.GetDateFormatMonthLarge(claimSentResponse.ManagementDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")), // ClaimDateEnglish,
                       _timeFormatService.GetDateTimeFormatMonthToltip(claimSentResponse.ManagementDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // ClaimDateToltip,
                       claimSentResponse.IsAnotherContraint,
                       claimSentResponse.AnotherContraint,
                       claimSentResponse.IsAvailableForAll,
                       claimSentResponse.MinimumMonthsConstraint.ToString(),
                       _timeFormatService.GetDateFormatMonthLarge(claimSentResponse.CreationDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // CloseDate,
                       _timeFormatService.GetDateFormatMonthLarge(claimSentResponse.CreationDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")), // CloseDateEnglish,
                       _timeFormatService.GetDateTimeFormatMonthToltip(claimSentResponse.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CloseDateToltip,
                       claimSentResponse.HasDeleted, //HasDeleted
                       _timeFormatService.GetDateFormatMonthLarge(claimSentResponse.DeletedDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // DeletedDate,
                       _timeFormatService.GetDateFormatMonthLarge(claimSentResponse.DeletedDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")), // DeletedDateEnglish,
                       _timeFormatService.GetDateTimeFormatMonthToltip(claimSentResponse.DeletedDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // DeletedDateToltip,
                       claimSentResponse.NameWhoDeletedBenefitClaim, // NameWhoDeletedClaim
                       claimSentResponse.NameWhoManagedClaim, // FullNameTh
                       _stringService.GetInitials(claimSentResponse.NameWhoManagedClaim),// ShortNameWhoChanged
                       CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Photo : string.Empty, // PhotoWhoChanged
                       claimSentResponse.Details,
                       claimSentResponse.CreationDate.Value
                    );

                    claimSentHistory.Add(temp);
                }

                if (claimSentHistory.Count > 0)
                {
                    ClaimSentResponse benefitClaimAnswerSentList = new
                    (
                        tempCollaborator.Id.Value.ToString(),
                        tempCollaborator.DocumentType is not null ? tempCollaborator.DocumentType.Name : string.Empty,
                        tempCollaborator.DocumentType is not null ? tempCollaborator.OtherDocumentType : string.Empty,
                        tempCollaborator.Document,
                        tempCollaborator.Name,
                        _timeFormatService.GetDateFormatMonthLarge(tempCollaborator.EntranceDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // EntranceDate,
                        _timeFormatService.GetDateFormatMonthLarge(tempCollaborator.EntranceDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")), // EntranceDateEnglish
                        tempCollaborator.Assignation.Name,
                        claimSentHistory.Count,
                        [.. claimSentHistory.OrderByDescending(x => x.CreacionDateFormat)]
                    );
                    items.Add(benefitClaimAnswerSentList);
                }
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