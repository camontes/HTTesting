using ErrorOr;
using HR_Platform.Application.NewCommunications.Common;
using HR_Platform.Application.NewCommunications.GetByCompanyId;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.NewCommunications;
using HR_Platform.Domain.Companies;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.NewCommunications.GetByCollaboratorId;

internal sealed class GetNewCommunicationByCompanyIdQueryHandler(
    INewCommunicationRepository newCommunicationRepository,
    ICompanyRepository companyRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetNewCommunicationByCompanyIdQuery, ErrorOr<List<NewCommunicationFileResponse>>>
{
    private readonly INewCommunicationRepository _newCommunicationRepository = newCommunicationRepository ?? throw new ArgumentNullException(nameof(newCommunicationRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<List<NewCommunicationFileResponse>>> Handle(GetNewCommunicationByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<NewCommunication>? newCommunicationListFull = await _newCommunicationRepository.GetByCompanyIdAsync(oldCompany.Id);
        List<NewCommunicationFileResponse> NewCommunicationListVisibles = [];
        List<NewCommunicationFileResponse> NewCommunicationListAll = [];

        if (newCommunicationListFull is not null && newCommunicationListFull.Count > 0)
        {
            foreach (NewCommunication item in newCommunicationListFull)
            {
                NewCommunicationFileResponse temp = new
                (
                   item.Id.Value, // IdFile
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", item.CreationDate.Value).Split('.')[0]), // TimePosted
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Creado", "Created", item.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
                   _timeFormatService.GetDateTimeFormatMonthToltip(item.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // TimePostedTolTip
                   _timeFormatService.GetDateTimeFormatMonthToltip(item.CreationDate.Value, "MMM dd yyyy HH:mm tt", new CultureInfo("en-US")), // TimePostedTolTipEnglish
                   item.Name, 
                   item.Description,
                   item.IsVisible,
                   _timeFormatService.GetDateFormatMonthLarge(item.CreationDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // CreatedFormat,
                   _timeFormatService.GetDateFormatMonthLarge(item.CreationDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")), // CreatedFormatEnglish,
                   _timeFormatService.GetDateFormatMonthLarge(item.EditionDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")), // UpdatedFormat,
                   _timeFormatService.GetDateFormatMonthLarge(item.EditionDate.Value, "MMMM dd yyyy", new CultureInfo("en-US")), // UpdatedFormatEnglish,
                   _timeFormatService.GetDateTimeFormatMonthToltip(item.EditionDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), //UpdateToltip
                   item.FileName,
                   item.FileURL,
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.CreationDateFile.Value).Split('.')[0]), // TimePostedFile
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.CreationDateFile.Value).Split('.')[1]), // TimePostedFileEnglish
                   item.ImageName,
                   item.ImageURL,
                   item.NameWhoChangedByTH, // FullNameTh
                   item.CreationDate.Value, // Creation date,
                   item.IsSurveyInclude //IsSurveyInclude
                );

                if (item.IsVisible)
                {
                    NewCommunicationListVisibles.Add(temp);
                }
                NewCommunicationListAll.Add(temp);
            }
        }
        return query.IsVisible ? NewCommunicationListVisibles.OrderByDescending(x => x.CreationDate).ToList(): [.. NewCommunicationListAll.OrderByDescending(x => x.CreationDate)];
    }
}