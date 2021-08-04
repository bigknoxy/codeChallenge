using System;

namespace Verikai.Sample.Service.Exceptions
{
    public class ReadFileException : Exception
    {
        public ReadFileException()
        {
        }

        public ReadFileException(string message)
            : base(message)
        {
        }

        public ReadFileException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
