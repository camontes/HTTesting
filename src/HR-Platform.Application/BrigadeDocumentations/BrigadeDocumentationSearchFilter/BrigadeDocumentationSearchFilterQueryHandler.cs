using ErrorOr;
using HR_Platform.Application.BrigadeDocumentations.Common;
using HR_Platform.Application.SearchFilters.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.BrigadeDocumentations;
using HR_Platform.Domain.SearchFilters;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.BrigadeDocumentations.BrigadeDocumentationSearchFilter;

internal sealed class BrigadeDocumentationSearchFilterQueryHandler(
    IBrigadeDocumentationRepository BrigadeDocumentationRepository,
    ITimeFormatService timeFormatService,
    IStringService stringService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<BrigadeDocumentationSearchFilterQuery, ErrorOr<SearchFilterResponse>>
{
    private readonly IBrigadeDocumentationRepository _brigadeDocumentationRepository = BrigadeDocumentationRepository ?? throw new ArgumentNullException(nameof(BrigadeDocumentationRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<SearchFilterResponse>> Handle(BrigadeDocumentationSearchFilterQuery query, CancellationToken cancellationToken)
    {
        var results = await _brigadeDocumentationRepository.SearchAsync(new BasicRequestSearch { Query = query.Query, Page = query.Page, PageSize = query.PageSize, Year = query.Year });

        List<BrigadeDocumentationFileResponse> items = [];

        if (results.TotalCount > 0)
        {
            foreach (BrigadeDocumentation item in results.Items)
            {
                BrigadeDocumentationFileResponse temp = new
                (
                    item.Id.Value, // IdFile
                    item.FileName, // FileName
                    item.UrlFile, // FileURL
                    String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.CreationDate.Value).Split('.')[0]), // TimePosted
                    String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
                    item.CreationDate.Value, // CreationDate
                    _timeFormatService.GetDateTimeFormatMonthToltip(item.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
                    item.UrlPhotoWhoChangedByTH, // UrlPhotoTH
                    item.NameWhoChangedByTH, // FullNameTh
                    _stringService.GetInitials(item.NameWhoChangedByTH) // ShortNameTh
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