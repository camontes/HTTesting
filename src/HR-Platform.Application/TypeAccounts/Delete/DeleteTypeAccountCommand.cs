using ErrorOr;
using MediatR;

namespace HR_Platform.Application.TypeAccounts.Delete;

public record DeleteTypeAccountCommand( Guid CompanyId, List<Guid> TypeAccountList) : IRequest<ErrorOr<bool>>;
