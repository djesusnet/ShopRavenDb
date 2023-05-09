using Microsoft.AspNetCore.Http;
using ShopRavenDb.Infrastructure.CrossCutting.Extensions;

namespace ShopRavenDb.Infrastructure.Data.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly IDocumentStore _documentStore;

    public DocumentRepository(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<string> AttachDocument(string version, string description, IEnumerable<Build> builds, IFormFile file)
    {
        VersioningFile? versioningFile = null;
        
        using (var session = _documentStore.OpenAsyncSession())
        {
            versioningFile = await session.Query<VersioningFile>()
                .Where(d => d.Version == version)
                .FirstOrDefaultAsync() ?? new VersioningFile()
            {
                CreateDate = DateTime.Now,
                Name = file.FileName,
                ContentType = file.ContentType,
                Extension = Path.GetExtension(file.FileName),
                Version = version,
                Description = description,
                CompressedContent = await file.OpenReadStream().CompressAsync(),
                Builds = builds
            };
            
            await session.StoreAsync(versioningFile);
            using var stream = new MemoryStream(versioningFile.CompressedContent);
            session.Advanced.Attachments.Store(versioningFile.Id, versioningFile.Name, stream, versioningFile.ContentType);
            await session.SaveChangesAsync();
        }
    
        return "Document successfully attached!";
    }
}