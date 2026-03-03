using ErrorOr;
using HR_Platform.Domain.Contracts;
using MediatR;

namespace Collaborators.UpdateContract;

public record UpdateContractsCommand(
    string EmailChangeBy, // TH 
    Guid CompanyId,
    string CollaboratorId,
    string Id,
    string PositionId, // Collaborador
    int AssignationTypeId, // Personal interno
    string AssignationId, // Fabrica
    string ContractTypeId, // Indefinido
    string Salary, // "1300000"
    int CurrencyTypeId, // Dolar
    string? Arl,
    string? Bonus
) : IRequest<ErrorOr<bool>>;

