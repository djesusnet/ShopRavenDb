

namespace ShopRavenDb.Domain.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }
    
    public async Task<string> AttachDocument(string version, string description, IEnumerable<Build> builds, IFormFile file)
    {
        return await _documentRepository.AttachDocument(version, description, builds, file).ConfigureAwait(false);
    }
}