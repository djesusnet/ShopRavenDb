namespace ShopRavenDb.Domain.Core.Interfaces.Services;

public interface IDocumentService
{
    Task<string> AttachDocument(IFormFile file);
    Task<AttachmentResult?> GetAttachDocument(string documentId);
}