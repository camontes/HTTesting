using ErrorOr;
using MediatR;
using HR_Platform.Application.DocumentTypes.Common;

namespace HR_Platform.Application.DocumentTypes.GetAll;

public record GetAllDocumentTypesQuery() : IRequest<ErrorOr<IReadOnlyList<DocumentTypesResponse>>>;