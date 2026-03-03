using ErrorOr;
using HR_Platform.Domain.Contracts;
using MediatR;

namespace Collaborators.UpdateContract;

public record UpdateBaseContractsCommand(
    string Id,
    string CollaboratorId,
    string PositionId, // Collaborador
    int AssignationTypeId, // Personal interno
    string AssignationId, // Fabrica
    string ContractTypeId, // Indefinido
    string Salary, //1300000.0
    int CurrencyTypeId, // Dolar
    string Arl, 
    string Bonus
) : IRequest<ErrorOr<bool>>;



