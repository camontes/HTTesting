using ErrorOr;
using HR_Platform.Application.BrigadeInventories.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.BrigadeInventoryFiles;
using HR_Platform.Domain.Companies;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.BrigadeInventories.GetByCompanyId;

internal sealed class GetBrigadeInventoryByCompanyIdQueryHandler(
    IBrigadeInventoryRepository brigadeInventoryRepository,
    ICompanyRepository companyRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetBrigadeInventoryByCompanyIdQuery, ErrorOr<FullBrigadeInventoryResponse>>
{
    private readonly IBrigadeInventoryRepository _brigadeInventoryRepository = brigadeInventoryRepository ?? throw new ArgumentNullException(nameof(brigadeInventoryRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<FullBrigadeInventoryResponse>> Handle(GetBrigadeInventoryByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<BrigadeInventory>? brigadeInventoryListFull = await _brigadeInventoryRepository.GetByCompanyIdAsync(oldCompany.Id, query.Year);
        List<BrigadeInventoryResponse> brigadeInventoryListAll = [];
        List<string> distinctYears = [];

        if (brigadeInventoryListFull is not null && brigadeInventoryListFull.Count > 0)
        {
            foreach (BrigadeInventory item in brigadeInventoryListFull)
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

            distinctYears = brigadeInventoryListFull
                .Select(obj => obj.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        FullBrigadeInventoryResponse response = new
        (
            brigadeInventoryListAll.OrderByDescending(e => e.EditionDate > e.CreationDate ? e.EditionDate : e.CreationDate).ToList(),
            distinctYears
        );

        return response;
    }
}