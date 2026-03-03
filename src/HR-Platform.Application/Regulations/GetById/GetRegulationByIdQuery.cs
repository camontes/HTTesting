using ErrorOr;
using HR_Platform.Application.Regulations.Common;
using MediatR;

namespace HR_Platform.Application.Regulations.GetById;

public record GetRegulationByIdQuery(Guid RegulationId) : IRequest<ErrorOr<RegulationFileResponse>>;