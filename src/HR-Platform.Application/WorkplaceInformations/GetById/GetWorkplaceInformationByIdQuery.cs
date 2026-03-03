using ErrorOr;
using HR_Platform.Application.WorkplaceInformations.Common;
using MediatR;

namespace HR_Platform.Application.WorkplaceInformations.GetById;

public record GetWorkplaceInformationByIdQuery(Guid WorkplaceInformationId) : IRequest<ErrorOr<WorkplaceInformationFileResponse>>;