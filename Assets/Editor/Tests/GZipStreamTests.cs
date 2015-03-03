using System.IO;
using Unity.IO.Compression;
using NUnit.Framework;

[TestFixture]
public class GZipStreamTests 
{
    private string Text = "This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this.";

    private byte[] CompressToArray()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
            using (var writer = new StreamWriter(gzipStream))
            {
                writer.Write(Text);
            }
            return memoryStream.ToArray();
        }
    }

    [Test]
    public void Compress()
    {
        var bytes = CompressToArray();
        Assert.AreEqual(235, bytes.Length);
    }

    [Test]
    public void Decompress()
    {
        var bytes = CompressToArray();

        using (var memoryStream = new MemoryStream(bytes))
        using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
        using (var reader = new StreamReader(gzipStream))
        {
            string text = reader.ReadToEnd();
            Assert.AreEqual(Text, text);
        }
    }
}
