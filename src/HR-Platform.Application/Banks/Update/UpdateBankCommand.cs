using ErrorOr;
using HR_Platform.Application.Banks.Common;
using MediatR;

namespace HR_Platform.Application.Banks.Update;

public record UpdateBankCommand
(
    Guid Id,

    string CompanyId,

    string Name,
    string NameEnglish
) : IRequest<ErrorOr<bool>>;
