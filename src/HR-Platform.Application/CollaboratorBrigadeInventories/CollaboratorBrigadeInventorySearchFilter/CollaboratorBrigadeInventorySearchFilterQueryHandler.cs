using ErrorOr;
using HR_Platform.Application.CollaboratorBrigadeInventories.Common;
using HR_Platform.Application.SearchFilters.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.BrigadeMembers;
using HR_Platform.Domain.CollaboratorBrigadeInventories;
using HR_Platform.Domain.CollaboratorBrigadeInventoryFiles;
using HR_Platform.Domain.CollaboratorBrigades;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.SearchFilters;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.CollaboratorBrigadeInventories.CollaboratorBrigadeInventorySearchFilter;

internal sealed class CollaboratorBrigadeInventorySearchFilterQueryHandler(
    ICollaboratorBrigadeRepository CollaboratorBrigadeInventoryRepository,
    ITimeFormatService timeFormatService,
    IStringService stringService,
    IBrigadeMemberRepository brigadeMemberRepository,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<CollaboratorBrigadeInventorySearchFilterQuery, ErrorOr<SearchFilterResponse>>
{
    private readonly ICollaboratorBrigadeRepository _collaboratorBrigadeRepository = CollaboratorBrigadeInventoryRepository ?? throw new ArgumentNullException(nameof(CollaboratorBrigadeInventoryRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));
    private readonly IBrigadeMemberRepository _brigadeMemberRepository = brigadeMemberRepository ?? throw new ArgumentNullException(nameof(brigadeMemberRepository));

    public async Task<ErrorOr<SearchFilterResponse>> Handle(CollaboratorBrigadeInventorySearchFilterQuery query, CancellationToken cancellationToken)
    {
        var results = await _collaboratorBrigadeRepository.SearchAsync(new BasicRequestSearch { Query = query.Query, Page = query.Page, PageSize = query.PageSize, Year = query.Year });

        List<BrigadeStaffingResponse> items = [];
        List<BrigadeMember> brigadeMembers = await _brigadeMemberRepository.GetAll();

        if (results.TotalCount > 0)
        {
            var collaboratorsGroup = results.Items.GroupBy(x => x.Collaborator.Name);

            foreach (var group in collaboratorsGroup)
            {
                List<CollaboratorBrigadeInventoryResponse> collaboratorBrigadeInventoryListAll = [];

                Collaborator tempCollaborator = group.First().Collaborator;
                BrigadeAdjustment tempBrigadeAdjustment = group.First().BrigadeAdjustment;

                string fullNameTh = string.Empty;

                DateTime TimeUpdate = DateTime.MinValue;

                foreach (var inventary in group)
                {
                    List<CollaboratorBrigadeInventoryFileResponse> filesResponse = [];

                    if (inventary.CollaboratorBrigadeInventory.CollaboratorBrigadeInventoryFiles.Count > 0)
                    {
                        foreach (CollaboratorBrigadeInventoryFile fileData in inventary.CollaboratorBrigadeInventory.CollaboratorBrigadeInventoryFiles)
                        {
                            CollaboratorBrigadeInventoryFileResponse file = new
                            (
                                fileData.Id.Value,
                                fileData.FileName,
                                fileData.UrlFile,
                                String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", inventary.CreationDate.Value).Split('.')[0]), // TimePosted
                                String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", inventary.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
                                inventary.IsDeleteable
                            );

                            filesResponse.Add(file);
                        }
                    }

                    CollaboratorBrigadeInventoryResponse temp = new
                     (
                        inventary.Id.Value, // Id
                        inventary.CollaboratorBrigadeInventory.BrigadeInventory.Name, //NameInventary
                        inventary.CollaboratorBrigadeInventory.BrigadeInventory.Description, //Description
                        inventary.AmountByBrigader, //DeliveriedAmount
                        inventary.CollaboratorBrigadeInventory.UnitMeasure.Name, //DeliveriedAmountUnit
                        _timeFormatService.GetDateFormatMonthLarge(inventary.CollaboratorBrigadeInventory.DeliveryDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // DeliveryDate
                        _timeFormatService.GetDateFormatMonthLarge(inventary.CollaboratorBrigadeInventory.DeliveryDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")), // DeliveryDateEnglish
                        inventary.CollaboratorBrigadeInventory.ReturnDate.Value == DateTime.MinValue ? "No aplica" : _timeFormatService.GetDateFormatMonthLarge(inventary.CollaboratorBrigadeInventory.ReturnDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), //ReturnDate
                        inventary.CollaboratorBrigadeInventory.ReturnDate.Value == DateTime.MinValue ? "Does not apply" : _timeFormatService.GetDateFormatMonthLarge(inventary.CollaboratorBrigadeInventory.ReturnDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")), //ReturnDateEnglish
                        inventary.CollaboratorBrigadeInventory.Observations,
                        tempBrigadeAdjustment.Name,
                        false, // IsLastBrigade
                        inventary.CollaboratorBrigadeInventory.BrigadeInventory.IsDeleted,
                        filesResponse,
                        inventary.CreationDate.Value // Creation date,
                    );

                    if (inventary.CreationDate.Value > TimeUpdate)
                    {
                        TimeUpdate = inventary.CreationDate.Value;
                        fullNameTh = inventary.CollaboratorBrigadeInventory.NameWhoChangedByTH;
                    }

                    collaboratorBrigadeInventoryListAll.Add(temp);
                }

                BrigadeMember? belongCurrentBrigade = brigadeMembers.SingleOrDefault(x => x.CollaboratorId.Value == tempCollaborator.Id.Value);

                CollaboratorBrigadeInventoryResponse theMostRecent = collaboratorBrigadeInventoryListAll.Aggregate((latest, current) => current.CreationDate > latest.CreationDate ? current : latest);

                collaboratorBrigadeInventoryListAll = [.. collaboratorBrigadeInventoryListAll.OrderByDescending(x => x.CreationDate)];

                if (collaboratorBrigadeInventoryListAll.Count > 0 && belongCurrentBrigade is not null && belongCurrentBrigade.BrigadeAdjustment.Name == collaboratorBrigadeInventoryListAll[0].NameInventary)
                {
                    collaboratorBrigadeInventoryListAll[0] = collaboratorBrigadeInventoryListAll[0] with
                    {
                        IsLastBrigade = true
                    };
                }

                BrigadeStaffingResponse brigadeStaffing = new(
                        tempCollaborator.Id.Value,
                        fullNameTh, // FullNameTh
                        _timeFormatService.GetDateFormatMonthLarge(TimeUpdate, "dd MMMM yyyy", new CultureInfo("es-CO")), // TimeUpdate,
                        _timeFormatService.GetDateFormatMonthLarge(TimeUpdate, "MMMM dd, yyyy", new CultureInfo("en-US")), // TimeUpdatedEnglish,
                        _timeFormatService.GetDateTimeFormatMonthToltip(TimeUpdate, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // TimePostedTolTip
                        tempCollaborator.Name,
                        tempBrigadeAdjustment.Name,
                        tempCollaborator.Assignation.Name,
                        tempCollaborator.Document,
                        tempCollaborator.DocumentType is not null ? tempCollaborator.DocumentType.Name : string.Empty,
                        tempCollaborator.DocumentType is not null ? tempCollaborator.OtherDocumentType : string.Empty,
                        _timeFormatService.GetDateFormatMonthLarge(tempCollaborator.EntranceDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // EntranceDate
                        _timeFormatService.GetDateFormatMonthLarge(tempCollaborator.EntranceDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")), //EntranceDateEnglish
                        collaboratorBrigadeInventoryListAll
                    );

                items.Add(brigadeStaffing);
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