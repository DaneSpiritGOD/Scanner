using Archiving.Core.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Archiving.Operation.Ftp
{
    public interface IFtpClient
    {
        ValueTask<int> UploadAsync(string localFilePath, string remoteFileName);
    }

    internal class SimpleFtpClient : IFtpClient
    {
        private readonly string _ftpRoot;
        private readonly string _ftpUploadRoot;
        private readonly int _timeout;
        private readonly ICredentials _credentials;

        public SimpleFtpClient(
            string ftpRoot,
            string userName,
            string password = null,
            int timeout = 500)
        {
            _credentials = new NetworkCredential(StringNullOrEmptyException.Assert(userName, nameof(userName)), password);
            _ftpRoot = StringNullOrEmptyException.Assert(ftpRoot, nameof(ftpRoot)).Trim();
            _ftpUploadRoot = normalizedRoot(_ftpRoot);
            _timeout = NumberOutOfRangeException<int>.Assert(timeout, 0, FtpOptions.MaxFtpTimeout, nameof(timeout));
        }

        public async ValueTask<int> UploadAsync(string filePath, string remoteFileName)
        {
#if NETSTANDARD2_0
            var fileContents = File.ReadAllBytes(filePath);
#else
            var fileContents = await File.ReadAllBytesAsync(filePath);
#endif

            var len = fileContents.LongLength;
            var request = createUploadFtpReq(remoteFileName, len);

            using (Stream requestStream = request.GetRequestStream())
            {
#warning long 值强转 int
                await requestStream.WriteAsync(fileContents, 0, (int)len);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                return (int)response.StatusCode;
            }
        }

        private FtpWebRequest createUploadFtpReq(string fileName, long fileLength)
        {
            var request = (FtpWebRequest)WebRequest.Create(Path.Combine(_ftpUploadRoot, fileName));
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = _credentials;
            request.ReadWriteTimeout = _timeout;
            request.Timeout = _timeout;
            request.ContentLength = fileLength;
            return request;
        }

#if NETSTANDARD2_0
        private const string seperateChar = "/";
#else
        private const char seperateChar = '/';
#endif
        private static string normalizedRoot(string root)
        {
            var local = root.Trim();
            if (local.EndsWith(seperateChar)) return root;
            return string.Concat(local, seperateChar);
        }
    }
}
