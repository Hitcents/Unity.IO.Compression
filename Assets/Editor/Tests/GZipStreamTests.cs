using System.IO;
using System.IO.Compression;
using NUnit.Framework;

[TestFixture]
public class GZipStreamTests 
{
    [Test]
    public void Test()
    {
        using (var memoryStream = new MemoryStream())
        using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Fastest))
        using (var writer = new StreamWriter(gzipStream))
        {

        }
    }
}
