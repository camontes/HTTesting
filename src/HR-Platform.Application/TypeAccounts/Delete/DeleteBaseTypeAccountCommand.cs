using ErrorOr;
using MediatR;

namespace HR_Platform.Application.TypeAccounts.Delete;

public record DeleteBaseTypeAccountCommand(List<Guid> TypeAccountList) : IRequest<ErrorOr<bool>>;
