namespace ShopRavenDb.Application.Interfaces;

public interface IDocumentApplication
{
    Task<string> AttachDocument(IFormFile file);
    Task<AttachmentResult?> GetAttachDocument(string documentId);
}