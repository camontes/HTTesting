using ErrorOr;
using HR_Platform.Application.UnitMeasures.Common;
using MediatR;

namespace HR_Platform.Application.UnitMeasures.GetAllNames;
public record GetAllUnitMeasuresNamesQuery() : IRequest<ErrorOr<IReadOnlyList<UnitMeasuresResponse>>>;