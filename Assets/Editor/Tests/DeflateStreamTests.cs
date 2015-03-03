using NUnit.Framework;
using System.IO;
using System.Net;
using Unity.IO.Compression;

[TestFixture]
public class DeflateStreamTests : BaseStreamTests
{
    protected override byte[] CompressToArray()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress))
            using (var writer = new StreamWriter(deflateStream))
            {
                writer.Write(Text);
            }
            return memoryStream.ToArray();
        }
    }

    //HACK: MS has a bug in their implementation when you try HTTP requests
    //  More info here: http://george.chiramattel.com/blog/2007/09/deflatestream-block-length-does-not-match.html
    protected override byte[] GetHttpBin(string url)
    {
        var request = HttpWebRequest.Create(url) as HttpWebRequest;
        request.Method = WebRequestMethods.Http.Get;
        request.Timeout = 10000;
        request.Accept = "*/*";
        request.Headers.Add("Accept-Encoding", "gzip, deflate");

        using (var response = request.GetResponse() as HttpWebResponse)
        using (var stream = response.GetResponseStream())
        {
            //HACK: skip 2 bytes
            stream.ReadByte();
            stream.ReadByte();

            byte[] buffer = new byte[response.ContentLength - 2];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }

    [Test]
    public void Compress()
    {
        var bytes = CompressToArray();
        Assert.AreEqual(217, bytes.Length);
    }

    [Test]
    public void Decompress()
    {
        var bytes = CompressToArray();

        using (var memoryStream = new MemoryStream(bytes))
        using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
        using (var reader = new StreamReader(deflateStream))
        {
            string text = reader.ReadToEnd();
            Assert.AreEqual(Text, text);
        }
    }

    [Test]
    public void WebRequest()
    {
        var bytes = GetHttpBin("http://httpbin.org/deflate");

        using (var memoryStream = new MemoryStream(bytes))
        using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
        using (var reader = new StreamReader(deflateStream))
        {
            string text = reader.ReadToEnd();
            Assert.IsTrue(text.Contains("\"deflated\": true"), "Request was not compressed!");
        }
    }
}
