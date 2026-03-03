using ErrorOr;
using HR_Platform.Application.Regulations.Common;
using MediatR;

namespace HR_Platform.Application.Regulations.GetByCompanyId;

public record GetBaseRegulationByCompanyIdQuery(string Year) : IRequest<ErrorOr<RegulationFileAndYearListResponse>>;