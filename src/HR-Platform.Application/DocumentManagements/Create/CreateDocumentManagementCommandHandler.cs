using ErrorOr;
using HR_Platform.Application.ContractTypes.Create;
using HR_Platform.Application.DocumentManagements.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DocumentManagementFileTypes;
using HR_Platform.Domain.DocumentManagements;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.DocumentManagements.Create;

internal sealed class CreateDocumentManagementsCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    IDocumentManagementRepository DocumentManagementRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateDocumentManagementsCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly IDocumentManagementRepository _documentManagementRepository = DocumentManagementRepository ?? throw new ArgumentNullException(nameof(DocumentManagementRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateDocumentManagementsCommand command, CancellationToken cancellationToken)
    {
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _collaboratorRepository.GetByIdAsync(new CollaboratorId(command.CollaboratorId)) is not Collaborator oldCollaborator)
            return Error.NotFound("Collaborator.NotFound", "The collaborator with the provide Id was not found.");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("DocumentManagements.CreationDate", "CreationDate is not valid");

        Collaborator? CollaboratorWhoChanged = await _collaboratorRepository.GetByEmailAsync(command.EmailChangeBy);

        List<DocumentManagement> documentManagementList = [];


        foreach (DocumentManagementFileFormatResponse item in command.FormatFiles)
        {
            DocumentManagement result = new
            (
                new DocumentManagementId(Guid.NewGuid()),
                oldCollaborator.Id,
                item.FileName,
                item.FileURL,
                command.EmailChangeBy,
                CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Name : string.Empty,
                CollaboratorWhoChanged is not null ? CollaboratorWhoChanged.Photo : string.Empty,
                new DocumentManagementFileTypeId(item.FileTypeId),
                item.Others,
                true,
                true,
                creationDate,
                creationDate
            );
            documentManagementList.Add(result);
        }
        if (documentManagementList.Count > 0)
        {
            _documentManagementRepository.AddRange(documentManagementList);
        }

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}