using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Banks.Delete;

public record DeleteBankCommand( List<Guid> BankList) : IRequest<ErrorOr<bool>>;

