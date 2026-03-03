using ErrorOr;
using HR_Platform.Application.TypeAccounts.Common;
using MediatR;

namespace HR_Platform.Application.TypeAccounts.Update;

public record UpdateTypeAccountCommand
(
    Guid Id,

    string CompanyId,

    string Name,
    string NameEnglish

) : IRequest<ErrorOr<bool>>;
