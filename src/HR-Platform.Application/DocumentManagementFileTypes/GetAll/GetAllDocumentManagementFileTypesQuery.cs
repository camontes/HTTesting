using ErrorOr;
using HR_Platform.Application.DocumentManagementFileTypes.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;

public record GetAllDocumentManagementFileTypesQuery() : IRequest<ErrorOr<IReadOnlyList<DocumentManagementFileTypesResponse>>>;