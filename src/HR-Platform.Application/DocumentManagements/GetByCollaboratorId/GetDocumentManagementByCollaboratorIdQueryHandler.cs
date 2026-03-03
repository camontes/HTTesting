using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using HR_Platform.Application.DocumentManagements.Common;
using HR_Platform.Application.DocumentManagements.GetByCompanyId;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DocumentManagements;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.DocumentManagements.GetByCollaboratorId;

internal sealed class GetDocumentManagementsByCollaboratorIdHandler(
    IDocumentManagementRepository DocumentManagementRepository,
    ICollaboratorRepository collaboratorRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetDocumentManagementsByCollaboratorIdQuery, ErrorOr<DocumentManagementsResponse>>
{
    private readonly IDocumentManagementRepository _documentManagementRepository = DocumentManagementRepository ?? throw new ArgumentNullException(nameof(DocumentManagementRepository));
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<DocumentManagementsResponse>> Handle(GetDocumentManagementsByCollaboratorIdQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(query.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

        List<DocumentManagement>? documentManagementList = await _documentManagementRepository.GetByCollaboratorIdAsync(new CollaboratorId(query.CollaboratorId));
        List<DocumentManagementFileResponse> filesList = [];

        if (documentManagementList is not null && documentManagementList.Count > 0)
        {
            foreach (DocumentManagement item in documentManagementList)
            {
                DocumentManagementFileResponse temp = new
                (
                   item.Id.Value, // IdFile
                   item.FileName, // FileName
                   item.UrlFile, // FileURL
                   item.DocumentManagementFileTypeId.Value, // DocumentManagementFileTypeId
                   item.DocumentManagementFileType.Name, // DocumentManagementFileType
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

        DocumentManagementsResponse documentManagementsResponse = new
        (
            oldCollaborator.Id.Value,
            oldCollaborator.Document,
            oldCollaborator.DocumentType is not null ? oldCollaborator.DocumentType.Name : string.Empty,
            oldCollaborator.Name,
            _timeFormatService.GetDateFormatMonthShort(oldCollaborator.EntranceDate.Value, "dd MMM yyyy", new CultureInfo("es-CO")),
            _timeFormatService.GetDateFormatMonthShort(oldCollaborator.EntranceDate.Value, "MMM dd yyyy", new CultureInfo("en-US")),
            [.. filesList.OrderByDescending(x => x.CreationDate)]
        );

        return documentManagementsResponse;

    }
}