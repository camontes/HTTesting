using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.BrigadeInventories.Create;

public record CreateBaseBrigadeInventoryCommand
(
    string Name,
    string Description,
    string CompanyLocation,
    int Amount,
    Guid UnitMeasureId,
    string? Other,
    bool ApplyPurchaseDate,
    bool ApplyExpirationDate,
    string? PurchaseDate,
    string? ExpirationDate,
    string? Observations,
    List<IFormFile>? BrigadeInventoryFiles

) : IRequest<ErrorOr<bool>>;


