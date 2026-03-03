using HR_Platform.Domain.BrigadeDocumentations;

namespace HR_Platform.Domain.ImprovementPlanResponseFiles;

public interface IImprovementPlanResponseFileRepository
{
    void AddRange(List<ImprovementPlanResponseFile> improvementPlanResponseFiles);
}
