namespace ShopRavenDb.Application.Dtos;

public class VersioningFileDto
{
    public DateTime CreateDate { get; set; }
    public string Name { get; set; }
    public string Version { get; set; }
    public string Description { get; set; }
    public IEnumerable<BuildDto> Builds { get; set; }
}