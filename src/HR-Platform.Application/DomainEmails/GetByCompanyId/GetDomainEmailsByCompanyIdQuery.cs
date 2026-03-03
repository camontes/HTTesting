using ErrorOr;
using MediatR;
using HR_Platform.Application.DomainEmails.Common;

namespace HR_Platform.Application.DomainEmails.GetByCompanyId;

public record GetDomainEmailsByCompanyIdQuery(Guid CompanyId) : IRequest<ErrorOr<List<DomainEmailsResponse>>>;