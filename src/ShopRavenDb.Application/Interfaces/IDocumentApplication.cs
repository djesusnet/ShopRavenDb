using Microsoft.AspNetCore.Http;

namespace ShopRavenDb.Application.Interfaces;

public interface IDocumentApplication
{
    Task<string> AttachDocument(string version, string description, IEnumerable<Build> builds, IFormFile file);
}