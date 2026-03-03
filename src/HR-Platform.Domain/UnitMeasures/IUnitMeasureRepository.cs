using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.UnitMeasures;

public interface IUnitMeasureRepository
{
    Task<List<UnitMeasure>> GetAll();
    Task<UnitMeasure?> GetByIdAsync(UnitMeasureId id);
    Task<UnitMeasure?> GetNoneUnitMeasureByCompanyIdAsync();
    Task<bool> ExistsAsync(UnitMeasureId id);
    Task<int> GetNumberOfUnitMeasures();
    void AddRangeUnitMeasures(List<UnitMeasure> UnitMeasure);
    void Add(UnitMeasure UnitMeasure);
    void Update(UnitMeasure UnitMeasure);
    void Delete(UnitMeasure UnitMeasure);
    void DeleteRange(List<UnitMeasure> tags);
}
