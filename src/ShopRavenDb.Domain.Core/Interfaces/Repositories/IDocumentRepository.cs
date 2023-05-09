namespace ShopRavenDb.Domain.Core.Interfaces.Repositories;

public interface IDocumentRepository
{
    Task<string> AttachDocument(string version, string description, IEnumerable<Build> builds, IFormFile file);
}