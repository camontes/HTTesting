using ErrorOr;
using HR_Platform.Application.BrigadeInventories.Common;
using HR_Platform.Application.BrigadeInventoriess.BrigadeInventorySearchFilter;
using HR_Platform.Application.SearchFilters.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.BrigadeInventoryFiles;
using HR_Platform.Domain.SearchFilters;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.BrigadeInventories.BrigadeInventorySearchFilter;

internal sealed class BrigadeInventorySearchFilterQueryHandler(
    IBrigadeInventoryRepository BrigadeInventoryRepository,
    ITimeFormatService timeFormatService,
    IStringService stringService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<BrigadeInventorySearchFilterQuery, ErrorOr<SearchFilterResponse>>
{
    private readonly IBrigadeInventoryRepository _brigadeInventoryRepository = BrigadeInventoryRepository ?? throw new ArgumentNullException(nameof(BrigadeInventoryRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<SearchFilterResponse>> Handle(BrigadeInventorySearchFilterQuery query, CancellationToken cancellationToken)
    {
        var results = await _brigadeInventoryRepository.SearchAsync(new BasicRequestSearch { Query = query.Query, Page = query.Page, PageSize = query.PageSize, Year = query.Year });

        List<BrigadeInventoryResponse> brigadeInventoryListAll = [];

        if (results.TotalCount > 0)
        {

            foreach (BrigadeInventory item in results.Items)
            {
                List<BrigadeInventoryFileResponse> filesResponse = [];

                if (item.BrigadeInventoryFiles.Count > 0)
                {
                    foreach (BrigadeInventoryFile fileData in item.BrigadeInventoryFiles)
                    {
                        BrigadeInventoryFileResponse file = new
                        (
                            fileData.Id.Value,
                            fileData.FileName,
                            fileData.UrlFile,
                            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", item.CreationDate.Value).Split('.')[0]), // TimePosted
                            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", item.CreationDate.Value).Split('.')[1]) // TimePostedEnglish

                        );
                        filesResponse.Add(file);
                    }
                }

                BrigadeInventoryResponse temp = new
                (
                   item.Id.Value, // Id
                   item.NameWhoChangedByTH, // FullNameTh
                   _timeFormatService.GetDateFormatMonthLarge(item.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // UpdatedFormat,
                   _timeFormatService.GetDateFormatMonthLarge(item.EditionDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")), // UpdatedFormatEnglish,
                   _timeFormatService.GetDateTimeFormatMonthToltip(item.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // TimePostedTolTip
                   _timeFormatService.GetDateTimeFormatMonthToltip(item.EditionDate.Value, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US")), // TimePostedTolTipEnglish

                   item.Name,
                   item.Description,
                   item.CompanyLocation,

                   item.Amount,
                   item.Amount - item.AvailableAmount,
                   item.AvailableAmount,
                   item.UnitMeasureId.Value,
                   item.UnitMeasure.Name,
                   item.UnitMeasure.NameEnglish,

                   item.PurchaseDate.Value == DateTime.MinValue ? "No aplica" : _timeFormatService.GetDateFormatMonthLarge(item.PurchaseDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // PurchaseDate
                   item.PurchaseDate.Value == DateTime.MinValue ? "Does not apply" : _timeFormatService.GetDateFormatMonthLarge(item.PurchaseDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")), // PurchaseDateEnglish
                   item.ExpirationDate.Value == DateTime.MinValue ? "No aplica" : _timeFormatService.GetDateFormatMonthLarge(item.ExpirationDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), //ExpirationDate
                   item.ExpirationDate.Value == DateTime.MinValue ? "Does not apply" : _timeFormatService.GetDateFormatMonthLarge(item.ExpirationDate.Value, "MMMM dd, yyyy", new CultureInfo("en-US")), //ExpirationDateEnglish

                   item.Observations,

                   item.IsDeleted,

                   item.CreationDate.Value, // Creation date,
                   item.EditionDate.Value, // EditionDate date,
                   filesResponse
                );

                brigadeInventoryListAll.Add(temp);
            }
        }
        return new SearchFilterResponse
        (
            results.TotalCount,
            brigadeInventoryListAll,
            results.TotalCount > 0 ? "Resultados encontrados." : "No se encontraron resultados."
        );
    }
}