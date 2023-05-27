namespace ShopRavenDb.Domain.Core.Interfaces.Repositories;

public interface IDocumentRepository
{
    Task<string> AttachDocument(IFormFile file);
    Task<AttachmentResult?> GetAttachDocument(string documentId);
}