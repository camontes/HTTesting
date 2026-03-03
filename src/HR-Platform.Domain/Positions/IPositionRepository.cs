using HR_Platform.Domain.Companies;
using System.Threading.Tasks;

namespace HR_Platform.Domain.Positions;

public interface IPositionRepository
{
    Task<List<Position>> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<Position?> GetCollaboratorPositionByCompanyIdAsync(CompanyId CompanyId);
    Task<Position?> GetByIdAsync(PositionId id);
    Task<Position?> GetByIdWithoutChildrenAsync(PositionId id);
    Task<bool> ExistsAsync(PositionId id);
    Task<List<Position>> GetAll();
    Task<List<Position>> GetDefaultsAsync();
    void Add(Position position);
    void Update(Position position);
    void Delete(Position position);
    void DeleteRange(List<Position> position);
}
