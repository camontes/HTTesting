using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using MediatR;

namespace HR_Platform.Application.DocumentManagements.GetByCompanyId;

public record GetDocumentManagementsByCollaboratorIdQuery(Guid CollaboratorId) : IRequest<ErrorOr<DocumentManagementsResponse>>;