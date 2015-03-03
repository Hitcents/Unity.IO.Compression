using NUnit.Framework;
using System;
using System.IO;
using Unity.IO.Compression;

[TestFixture]
public class GZipStreamTests : BaseStreamTests
{
    protected override byte[] CompressToArray()
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

    [Test]
    public void WebRequest()
    {
        var bytes = GetHttpBin("http://httpbin.org/gzip");

        using (var memoryStream = new MemoryStream(bytes))
        using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
        using (var reader = new StreamReader(gzipStream))
        {
            string text = reader.ReadToEnd();
            Assert.IsTrue(text.Contains("\"gzipped\": true"), "Request was not compressed!");
        }
    }

    [Test]
    public void LeaveOpenTrue()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            using (var writer = new StreamWriter(gzipStream))
            {
                writer.Write(Text);
            }

            using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            using (var writer = new StreamWriter(gzipStream))
            {
                writer.Write(Text);
            }
        }
    }

    [Test, ExpectedException(typeof(ArgumentException))]
    public void LeaveOpenFalse()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, false))
            using (var writer = new StreamWriter(gzipStream))
            {
                writer.Write(Text);
            }

            using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, false))
            using (var writer = new StreamWriter(gzipStream))
            {
                writer.Write(Text);
            }
        }
    }
}
