using ErrorOr;
using HR_Platform.Application.Inductions.Common;
using HR_Platform.Application.SearchFilters.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorGeneralInductions;
using HR_Platform.Domain.CollaboratorInductions;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.SearchFilters;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Globalization;

namespace HR_Platform.Application.Inductions.InductionSentSearchFilter;

internal sealed class InductionsSentSearchFilterQueryHandler
(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorGeneralInductionRepository collaboratorGeneralInductionRepository,
    ICollaboratorInductionRepository collaboratorInductionRepository,
    IInductionRepository inductionRepository,
    
    ITimeFormatService timeFormatService
)
:
IRequestHandler<InductionsSentSearchFilterQuery, ErrorOr<SearchFilterResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorGeneralInductionRepository _collaboratorGeneralInductionRepository = collaboratorGeneralInductionRepository ?? throw new ArgumentNullException(nameof(collaboratorGeneralInductionRepository));
    private readonly ICollaboratorInductionRepository _collaboratorInductionRepository = collaboratorInductionRepository ?? throw new ArgumentNullException(nameof(collaboratorInductionRepository));
    private readonly IInductionRepository _inductionRepository = inductionRepository ?? throw new ArgumentNullException(nameof(inductionRepository));
    
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<SearchFilterResponse>> Handle(InductionsSentSearchFilterQuery query, CancellationToken cancellationToken)
    {
        SearchFilter<Collaborator> results = await _collaboratorRepository.SearchAsyncWithoutPages
        (
            new BasicRequestSearch { Query = query.Query, Page = query.Page, PageSize = query.PageSize }
        );

        List<Collaborator> collaborators = results.Items.ToList();

        List<Induction>? inductionListFull = await _inductionRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId));

        List<InductionSentResponse> finalResponse = [];


        if (inductionListFull is not null && inductionListFull.Count > 0)
        {
            //Filter to exclude hidden inductions
            List<Induction> firstFilterByHiddenInduction = inductionListFull.Where(x => x.IsVisible).ToList();

            if (firstFilterByHiddenInduction.Count > 0)
            {
                foreach (Collaborator oldCollaborator in collaborators)
                {
                    List<InductionSentDetailResponse> detailInductionList = [];

                    //Collaborators who are allowed to see through the ** Green Eye ** 
                    List<CollaboratorInduction> collaboratorInductions = await _collaboratorInductionRepository
                                                                            .GetByCollaboratorIdAsync(oldCollaborator.Id);

                    //Collaborator who have completed their inductions
                    List<CollaboratorGeneralInduction> collaboratorGeneralInduction = await _collaboratorGeneralInductionRepository
                                                                                        .GetByCollaboratorIdAsync(oldCollaborator.Id);

                    foreach (Induction induction in firstFilterByHiddenInduction)
                    {
                        //Checks whether the Collaborator has already completed a specific induction
                        CollaboratorGeneralInduction? IsCollaboratorFinishedInduction = collaboratorGeneralInduction
                            .SingleOrDefault(x => x.CollaboratorId.Value == oldCollaborator.Id.Value
                                                    && x.InductionId.Value == induction.Id.Value);

                        //Has Access For Seeing a specific induction  **
                        CollaboratorInduction? HasAccessForSeeing = collaboratorInductions
                            .SingleOrDefault(x => x.CollaboratorId.Value == oldCollaborator.Id.Value
                                                    && x.InductionId.Value == induction.Id.Value);

                        //Filter the inductions that the Collaborator can see when the entry date is greater than the creation of the induction ***
                        List<Induction> filterByCreationDate = inductionListFull
                            .Where(x => x.CreationDate.Value < oldCollaborator.CreationDate.Value
                                        && x.AllowForAllCollaborators)
                            .ToList();

                        // If the collaborator has the green eye option, the date validation does not enter.
                        List<Induction> filterByGreenEye = inductionListFull
                            .Where(x => !x.AllowForAllCollaborators
                                        && HasAccessForSeeing is not null)
                            .ToList();

                        List<Induction> secondFilterByCreationDate = [.. filterByCreationDate, .. filterByGreenEye];

                        if (secondFilterByCreationDate.Count > 0 && !induction.IsInductionDeleted && IsCollaboratorFinishedInduction is null)
                        {
                            InductionSentDetailResponse temp = new
                            (
                                induction.Name,
                                _timeFormatService.GetDateFormatMonthLarge(induction.IsVisibleChangeDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // ShipmentDateDate,
                                _timeFormatService.GetDateFormatMonthLarge(induction.IsVisibleChangeDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")), // ShipmentDateEnglish,
                                _timeFormatService.GetDateTimeFormatMonthToltip(induction.IsVisibleChangeDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
                                induction.IsVisibleChangeDate.Value,
                                induction.AllowForAllCollaborators
                            );

                            if (temp.AllowForAllCollaborators)
                            {
                                detailInductionList.Add(temp);
                            }
                            else
                            {
                                if (HasAccessForSeeing is not null)
                                {
                                    detailInductionList.Add(temp);
                                }
                            }
                        }
                    }

                    if (detailInductionList.Count > 0)
                    {
                        InductionSentResponse inductionSentList = new
                        (
                            oldCollaborator.Id.Value.ToString(),
                            oldCollaborator.DocumentType is not null ? oldCollaborator.DocumentType.Name : "C.C",
                            oldCollaborator.Document,
                            oldCollaborator.Name,
                            oldCollaborator.EntranceDate.Value,
                            _timeFormatService.GetDateFormatMonthLarge(oldCollaborator.EntranceDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // EntranceDate,
                            _timeFormatService.GetDateFormatMonthLarge(oldCollaborator.EntranceDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")), // EntranceDateEnglish
                            oldCollaborator.Assignation.Name,
                            detailInductionList.Count,
                            detailInductionList
                        );
                        finalResponse.Add(inductionSentList);
                    }
                }
            }
        }

        results.TotalCount = finalResponse.Count;

        List<InductionSentResponse> items = query.Page == 0 || query.PageSize == 0
        ? [.. finalResponse]
        : finalResponse
        .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize).ToList();

        return new SearchFilterResponse
        (
            results.TotalCount,
            items.OrderByDescending(x => x.DetailInductionList.Max(i => i.ShipmentDateFormat)),
            results.TotalCount > 0 ? "Resultados encontrados." : "No se encontraron resultados."
        );
    }
}
