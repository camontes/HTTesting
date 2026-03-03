using ErrorOr;
using HR_Platform.Application.DocumentManagements.Common;
using MediatR;

namespace HR_Platform.Application.ContractTypes.Create;

public record CreateDocumentManagementsCommand(
    string EmailChangeBy,
    Guid CollaboratorId,
    List<DocumentManagementFileFormatResponse> FormatFiles
) : IRequest<ErrorOr<bool>>;



