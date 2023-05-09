using System.IO.Compression;

namespace ShopRavenDb.Infrastructure.CrossCutting.Extensions;

public static class StreamExtensions
{
    public static async Task<byte[]> CompressAsync(this Stream stream)
    {
        using var memoryStream = new MemoryStream();
        await using var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress);
        await stream.CopyToAsync(gzipStream);
        gzipStream.Close();
        return memoryStream.ToArray();
    }
}