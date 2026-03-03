using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using MediatR;

namespace HR_Platform.Application.OccupationalTests.GetByCompanyId;

public record GetOccupationalTestsByCollaboratorIdQuery(Guid CollaboratorId, string Year) : IRequest<ErrorOr<OccupationalTestsResponse>>;