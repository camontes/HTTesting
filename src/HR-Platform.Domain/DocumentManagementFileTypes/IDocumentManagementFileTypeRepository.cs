namespace HR_Platform.Domain.DocumentManagementFileTypes;

public interface IDocumentManagementFileTypeRepository
{
    Task<List<DocumentManagementFileType>> GetAll();
}
