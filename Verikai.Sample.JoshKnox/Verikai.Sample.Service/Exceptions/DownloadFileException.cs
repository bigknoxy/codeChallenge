using System;

namespace Verikai.Sample.Service.Exceptions
{
    public class DownloadFileException : Exception
    {
        public DownloadFileException()
        {
        }

        public DownloadFileException(string message)
            : base(message)
        {
        }

        public DownloadFileException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
