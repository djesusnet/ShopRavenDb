namespace ShopRavenDb.Domain.Model;

public class VersioningFile
{
    public string Id { get; set; }
    public DateTime CreateDate { get; set; }
    public string Name { get; set; }
    public string ContentType { get; set; }
    public string Extension { get; set; }
    public string Version { get; set; }
    public string Description { get; set; }
    public byte[] CompressedContent { get; set; }
    public IEnumerable<Build> Builds { get; set; }
}