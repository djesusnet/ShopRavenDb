namespace ShopRavenDb.Domain.Core.Interfaces.Services;

public interface IDocumentService
{
    Task<string> AttachDocument(string version, string description, IEnumerable<Build> builds, IFormFile file);
}