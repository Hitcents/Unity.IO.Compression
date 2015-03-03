using System.IO;
using Unity.IO.Compression;
using NUnit.Framework;

[TestFixture]
public class GZipStreamTests 
{
    private string Text = "This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this.";

    [Test]
    public void GZipFastest()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Fastest))
            using (var writer = new StreamWriter(gzipStream))
            {
                writer.Write(Text);
            }
            memoryStream.Flush();

            var bytes = memoryStream.ToArray();
            Assert.AreEqual(235, bytes.Length);
        }
    }

    [Test]
    public void GZipOptimal()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
            using (var writer = new StreamWriter(gzipStream))
            {
                writer.Write(Text);
            }
            memoryStream.Flush();

            var bytes = memoryStream.ToArray();
            Assert.AreEqual(235, bytes.Length);
        }
    }

    [Test]
    public void GZipNoCompression()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.NoCompression))
            using (var writer = new StreamWriter(gzipStream))
            {
                writer.Write(Text);
            }
            memoryStream.Flush();

            var bytes = memoryStream.ToArray();
            Assert.AreEqual(235, bytes.Length);
        }
    }
}
