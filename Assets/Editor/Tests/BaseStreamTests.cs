using NUnit.Framework;
using System;
using System.IO;
using System.Net;
using Unity.IO.Compression;

public class BaseStreamTests
{
    protected string Text = "This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this. This is example text that we might want to GZip because it is really long and weird. I would be embarrassed to read this.";

    protected virtual byte[] CompressToArray()
    {
        throw new NotImplementedException();
    }

    protected virtual byte[] GetHttpBin(string url)
    {
        var request = HttpWebRequest.Create(url) as HttpWebRequest;
        request.Method = WebRequestMethods.Http.Get;
        request.Timeout = 10000;
        request.Accept = "*/*";
        request.Headers.Add("Accept-Encoding", "gzip, deflate");

        using (var response = request.GetResponse() as HttpWebResponse)
        using (var stream = response.GetResponseStream())
        {
            byte[] buffer = new byte[response.ContentLength];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}
