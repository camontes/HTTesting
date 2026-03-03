using ErrorOr;
using HR_Platform.Application.ActiveBreaks.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.ActiveBreaks;
using HR_Platform.Domain.Companies;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.ActiveBreaks.GetByCompanyId;

internal sealed class GetActiveBreaksByCompanyIdQueryHandler
(
    IActiveBreakRepository activeBreakRepository,
    ICompanyRepository companyRepository,

    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

)
:
IRequestHandler<GetActiveBreaksByCompanyIdQuery, ErrorOr<List<ActiveBreakResponse>>>
{
    private readonly IActiveBreakRepository _activeBreakRepository = activeBreakRepository ?? throw new ArgumentNullException(nameof(activeBreakRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<List<ActiveBreakResponse>>> Handle(GetActiveBreaksByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company company)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<ActiveBreak>? activeBreaks = await _activeBreakRepository.GetByCompanyIdAsync(company.Id);

        List<ActiveBreakResponse> activeBreaksResponse = [];

        if (activeBreaks is not null && activeBreaks.Count > 0)
        {
            foreach (ActiveBreak activeBreak in activeBreaks)
            {
                ActiveBreakResponse activeBreakReponse = new
                (
                   activeBreak.Id.Value,

                   activeBreak.Name,
                   activeBreak.Description,

                   activeBreak.IsVisible,
                   activeBreak.IsPinned,

                   activeBreak.IsEditable,
                   activeBreak.IsDeleteable,

                   activeBreak.EmailWhoChangedByHR,
                   activeBreak.NameWhoChangedByHR,

                   activeBreak.CreationDate.Value,

                   string.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", activeBreak.CreationDate.Value).Split('.')[0]), // CreatedFormat
                   string.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", activeBreak.CreationDate.Value).Split('.')[1]), // CreatedFormatEnglish

                   _timeFormatService.GetDateTimeFormatMonthToltip(activeBreak.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreatedTolTip
                   _timeFormatService.GetDateTimeFormatMonthToltip(activeBreak.CreationDate.Value, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US")), // CreatedTolTipEnglish

                   activeBreak.EditionDate.Value,

                   _timeFormatService.GetDateFormatMonthLarge(activeBreak.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // EditedFormat
                   _timeFormatService.GetDateFormatMonthLarge(activeBreak.EditionDate.Value, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US")), // EditedFormatEnglish

                   _timeFormatService.GetDateTimeFormatMonthToltip(activeBreak.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // EditedTolTip
                   _timeFormatService.GetDateTimeFormatMonthToltip(activeBreak.CreationDate.Value, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US")), // EditedTolTipEnglish

                   activeBreak.File,
                   activeBreak.FileName,

                   activeBreak.CreationDateFile != null ? activeBreak.CreationDateFile.Value : null,

                   activeBreak.CreationDateFile != null 
                       ? string.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", activeBreak.CreationDateFile.Value).Split('.')[0])
                       : string.Empty, // CreatedFileFormat
                   activeBreak.CreationDateFile != null
                       ? string.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", activeBreak.CreationDateFile.Value).Split('.')[1])
                       : string.Empty, // CreatedFileFormatEnglish

                   activeBreak.CreationDateFile != null
                       ?  _timeFormatService.GetDateTimeFormatMonthToltip(activeBreak.CreationDateFile.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO"))
                       : string.Empty, // CreatedFileTolTip
                   activeBreak.CreationDateFile != null
                       ? _timeFormatService.GetDateTimeFormatMonthToltip(activeBreak.CreationDateFile.Value, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US"))
                       : string.Empty, // CreatedFileTolTipEnglish

                   activeBreak.Image,
                   activeBreak.ImageName,

                   activeBreak.CreationDateImage != null ? activeBreak.CreationDateImage.Value : null,

                   activeBreak.CreationDateImage != null
                       ? string.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", activeBreak.CreationDateImage.Value).Split('.')[0])
                       : string.Empty, // CreatedImageFormat
                   activeBreak.CreationDateImage != null
                       ? string.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", activeBreak.CreationDateImage.Value).Split('.')[1])
                       : string.Empty, // CreatedImageFormatEnglish
                       
                   activeBreak.CreationDateImage != null
                       ? _timeFormatService.GetDateTimeFormatMonthToltip(activeBreak.CreationDateImage.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO"))
                       : string.Empty, // CreatedImageTolTip
                   activeBreak.CreationDateImage != null
                       ? _timeFormatService.GetDateTimeFormatMonthToltip(activeBreak.CreationDateImage.Value, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US"))
                       : string.Empty // CreatedImageTolTipEnglish
                );

                activeBreaksResponse.Add(activeBreakReponse);
            }
        }
        return activeBreaksResponse.OrderByDescending(x => x.CreationDate).ToList();
    }
}