using System;

namespace FormatConverter.Convert.Reader
{
    public class FileTypeNotSupportedException : Exception
    {
        public FileTypeNotSupportedException(string message)
            :base(message)
        {
            
        }
    }
}