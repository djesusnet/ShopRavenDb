namespace ShopRavenDb.Application;

public class DocumentApplication : IDocumentApplication
{
    private readonly IDocumentService _documentService;

    public DocumentApplication(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public async Task<string> AttachDocument(IFormFile file)
    {
        return await _documentService.AttachDocument(file).ConfigureAwait(false);
    }

    public async Task<AttachmentResult?> GetAttachDocument(string documentId)
    {
        return await _documentService.GetAttachDocument(documentId).ConfigureAwait(false);
    }
}