namespace HR_Platform.Domain.DocumentTypes;

public interface IDocumentTypeRepository
{
    Task<List<DocumentType>> GetAll();
    Task<DocumentType?> GetByIdAsync(DocumentTypeId value);
}
