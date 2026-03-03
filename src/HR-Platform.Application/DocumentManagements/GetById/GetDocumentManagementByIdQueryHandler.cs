using ErrorOr;
using HR_Platform.Application.DocumentManagements.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.DocumentManagements;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.DocumentManagements.GetById;

internal sealed class GetByIdQueryHandler(
    IDocumentManagementRepository minuteRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference
    ) : IRequestHandler<GetDocumentManagementByIdQuery, ErrorOr<DocumentManagementFileResponse>>
{
    private readonly IDocumentManagementRepository _minuteRepository = minuteRepository ?? throw new ArgumentNullException(nameof(minuteRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<DocumentManagementFileResponse>> Handle(GetDocumentManagementByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _minuteRepository.GetByIdAsync(new DocumentManagementId(query.DocumentManagementId)) is not DocumentManagement oldDocumentManagement)
            return Error.NotFound("DocumentManagement.GetById", "The Document Management with the provide id was not found");

        DocumentManagementFileResponse response = new
        (
            oldDocumentManagement.Id.Value, // IdFile
            oldDocumentManagement.FileName, // FileName
            oldDocumentManagement.UrlFile, // FileURL
            oldDocumentManagement.DocumentManagementFileTypeId.Value, // DocumentManagementFileTypeId
            oldDocumentManagement.DocumentManagementFileType.Name, // DocumentManagementFileType
            oldDocumentManagement.Other, // Other
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldDocumentManagement.CreationDate.Value).Split('.')[0]), // TimePosted
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldDocumentManagement.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
            oldDocumentManagement.CreationDate.Value, // CreationDate
            _timeFormatService.GetDateTimeFormatMonthToltip(oldDocumentManagement.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
            oldDocumentManagement.UrlPhotoWhoChangedByTH, // UrlPhotoTH
            oldDocumentManagement.NameWhoChangedByTH, // FullNameTh
            _stringService.GetInitials(oldDocumentManagement.NameWhoChangedByTH) // ShortNameTh
        );

        return response;
    }
}