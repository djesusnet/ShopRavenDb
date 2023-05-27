using Microsoft.AspNetCore.Http;
using Raven.Client.Documents.Operations.Attachments;

namespace ShopRavenDb.Infrastructure.Data.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly IDocumentStore _documentStore;

    public DocumentRepository(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<string> AttachDocument(IFormFile file)
    {
        Document? document = null;

        using var session = _documentStore.OpenAsyncSession();
        document = await session.Query<Document>()
                                .FirstOrDefaultAsync(d => d.Name == file.FileName).ConfigureAwait(false) ?? new Document()
        {
            CreateDate = DateTime.Now,
            Name = file.FileName
        };
        await session.StoreAsync(document).ConfigureAwait(false);
        await using var stream = file.OpenReadStream();
        session.Advanced.Attachments.Store(document.Id, document.Name, stream, file.ContentType);
        await session.SaveChangesAsync().ConfigureAwait(false);
        
        return "Document sucessfully attached !";
    }

    public async Task<AttachmentResult?> GetAttachDocument(string documentId)
    {
     
        using var session = _documentStore.OpenAsyncSession();

        var document = await session.LoadAsync<Document>(documentId).ConfigureAwait(false);

        if (document is null) return null;
        
        var attachment = await session.Advanced.Attachments.GetAsync(document.Id, document.Name).ConfigureAwait(false);
        return attachment;

    }
}