using Microsoft.AspNetCore.Http;

namespace ShopRavenDb.Application;

public class DocumentApplication : IDocumentApplication
{
    private readonly IDocumentService _documentService;

    public DocumentApplication(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public async Task<string> AttachDocument(string version, string description, IEnumerable<Build> builds, IFormFile file)
    {
        return await _documentService.AttachDocument(version, description, builds, file).ConfigureAwait(false);
    }
}