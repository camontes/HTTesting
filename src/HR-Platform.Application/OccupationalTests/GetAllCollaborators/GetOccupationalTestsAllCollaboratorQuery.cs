using ErrorOr;
using HR_Platform.Application.OccupationalTests.Common;
using MediatR;

namespace HR_Platform.Application.OccupationalTests.GetByCompanyId;

public record GetOccupationalTestsAllCollaboratorQuery(int Page, int PageSize) : IRequest<ErrorOr<AllCollaboratorsResponse>>;