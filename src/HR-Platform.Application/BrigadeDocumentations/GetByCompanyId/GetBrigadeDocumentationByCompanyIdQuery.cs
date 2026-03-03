using ErrorOr;
using HR_Platform.Application.BrigadeDocumentations.Common;
using MediatR;

namespace HR_Platform.Application.BrigadeDocumentations.GetByCompanyId;

public record GetBrigadeDocumentationByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<BrigadeDocumentationFileAndYearListResponse>>;