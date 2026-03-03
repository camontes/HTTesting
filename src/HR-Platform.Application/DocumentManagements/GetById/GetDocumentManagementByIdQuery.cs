using ErrorOr;
using HR_Platform.Application.DocumentManagements.Common;
using MediatR;

namespace HR_Platform.Application.DocumentManagements.GetById;

public record GetDocumentManagementByIdQuery(Guid DocumentManagementId) : IRequest<ErrorOr<DocumentManagementFileResponse>>;