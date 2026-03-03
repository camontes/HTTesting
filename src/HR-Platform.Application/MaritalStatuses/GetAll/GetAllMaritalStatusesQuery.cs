using ErrorOr;
using HR_Platform.Application.MaritalStatuses.Common;
using MediatR;

namespace HR_Platform.Application.MaritalStatuses.GetAll;

public record GetAllMaritalStatusesQuery() : IRequest<ErrorOr<List<MaritalStatusesResponse>>>;