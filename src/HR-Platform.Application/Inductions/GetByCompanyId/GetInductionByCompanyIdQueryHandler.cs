using ErrorOr;
using HR_Platform.Application.Inductions.Common;
using HR_Platform.Application.Inductions.GetByCompanyId;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.InductionFiles;
using HR_Platform.Domain.Inductions;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Inductions.GetByCollaboratorId;

internal sealed class GetInductionByCompanyIdQueryHandler(
    IInductionRepository inductionRepository,
    ICompanyRepository companyRepository,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetInductionByCompanyIdQuery, ErrorOr<List<InductionResponse>>>
{
    private readonly IInductionRepository _inductionRepository = inductionRepository ?? throw new ArgumentNullException(nameof(inductionRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<List<InductionResponse>>> Handle(GetInductionByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        List<Induction>? inductionListFull = await _inductionRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId));
        List<InductionResponse> response = [];

        if (inductionListFull is not null && inductionListFull.Count > 0)
        {
            foreach (Induction item in inductionListFull)
            {
                if (!item.IsInductionDeleted)
                {
                    List<InductionFileResponse> filesList = [];
                    foreach (InductionFile file in item.InductionFiles)
                    {
                        InductionFileResponse fileTemp = new
                        (
                            file.Id.Value,
                            file.FileName,
                            file.UrlFile,
                            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.CreationDate.Value).Split('.')[0]), // TimePosted
                            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
                            _timeFormatService.GetDateFormatMonthLarge(item.CreationDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // CreationDate,
                            _timeFormatService.GetDateFormatMonthLarge(item.CreationDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")), // CreationDateEnglish,
                            _timeFormatService.GetDateTimeFormatMonthToltip(item.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")) // CreationDateTooltip,
                        );
                        filesList.Add(fileTemp);
                    }

                    InductionResponse temp = new
                    (
                       item.Id.Value.ToString(),
                       item.Name,
                       item.Description,
                       _timeFormatService.GetDateFormatMonthLarge(item.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // UpdatedFormat,
                       _timeFormatService.GetDateFormatMonthLarge(item.EditionDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")), // UpdatedFormatEnglish,
                       _timeFormatService.GetDateTimeFormatMonthToltip(item.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
                       item.NameWhoChangedByTH, // FullNameTh
                       item.IsVisible,
                       item.AllowForAllCollaborators,
                       item.CreationDate.Value,
                       filesList
                    );
                    response.Add(temp);
                }
            }

        }

        return response.OrderByDescending(x => x.CreacionDate).ToList();

    }
}
