using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using HR_Platform.Application.OccupationalTests.Common;
using HR_Platform.Application.OccupationalTests.GetByCompanyId;
using HR_Platform.Application.Services;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.OccupationalTests;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.OccupationalTests.GetByCollaboratorId;

internal sealed class GetOccupationalTestsByCollaboratorIdHandler(
    IOccupationalTestRepository OccupationalTestRepository,
    ICollaboratorRepository collaboratorRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetOccupationalTestsByCollaboratorIdQuery, ErrorOr<OccupationalTestsResponse>>
{
    private readonly IOccupationalTestRepository _occupationalTestRepository = OccupationalTestRepository ?? throw new ArgumentNullException(nameof(OccupationalTestRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<OccupationalTestsResponse>> Handle(GetOccupationalTestsByCollaboratorIdQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(query.CollaboratorId)) is not Collaborator oldCollaborator)
        {
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");
        }

        List<OccupationalTest>? occupationalTestListFull = await _occupationalTestRepository.GetByCollaboratorIdAsync(new CollaboratorId(query.CollaboratorId));
        List<OccupationalTest>? occupationalTestList = occupationalTestListFull?.Where(h => h.CreationDate.Value.Year.ToString() == query.Year).ToList();
        List<OccupationalTestFileResponse> filesList = [];
        List<string> distinctYears = [];

        if (occupationalTestList is not null && occupationalTestList.Count > 0)
        {
            foreach (OccupationalTest item in occupationalTestList)
            {
                OccupationalTestFileResponse temp = new
                (
                   item.Id.Value, // IdFile
                   item.FileName, // FileName
                   item.UrlFile, // FileURL
                   item.DefaultFileTypeId.Value, // FileTypeId
                   item.DefaultFileType.Name, // FileTypeName
                   item.Other, // Other
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.CreationDate.Value).Split('.')[0]), // TimePosted
                   String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", item.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
                   item.CreationDate.Value, // CreationDate
                   _timeFormatService.GetDateTimeFormatMonthToltip(item.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
                   item.UrlPhotoWhoChangedByTH, // UrlPhotoTH
                   item.NameWhoChangedByTH, // FullNameTh
                   _stringService.GetInitials(item.NameWhoChangedByTH) // ShortNameTh
                );

                filesList.Add(temp);
            }
        }

        if (occupationalTestListFull is not null)
        {
            distinctYears = occupationalTestListFull
                .Select(obj => obj.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        OccupationalTestsResponse occupationalTestsResponse = new
        (
            oldCollaborator.Id.Value,
            oldCollaborator.Document,
            oldCollaborator.DocumentType is not null ? oldCollaborator.DocumentType.Name : string.Empty,
            oldCollaborator.OtherDocumentType is not null ? oldCollaborator.OtherDocumentType : string.Empty,
            oldCollaborator.Name,
            _timeFormatService.GetDateFormatMonthShort(oldCollaborator.EntranceDate.Value, "dd MMM yyyy", new CultureInfo("es-CO")),
            filesList,
            distinctYears
        );

        return occupationalTestsResponse;

    }
}