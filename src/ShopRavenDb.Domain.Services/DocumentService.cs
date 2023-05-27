using Raven.Client.Documents.Operations.Attachments;

namespace ShopRavenDb.Domain.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<string> AttachDocument(IFormFile file)
    {
        return await _documentRepository.AttachDocument(file).ConfigureAwait(false);
    }

    public async Task<AttachmentResult?> GetAttachDocument(string documentId)
    {
        return await _documentRepository.GetAttachDocument(documentId).ConfigureAwait(false);
    }
}