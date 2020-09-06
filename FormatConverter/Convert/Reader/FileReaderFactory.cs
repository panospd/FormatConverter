using System.Collections.Generic;
using System.IO;
using System.Linq;
using FormatConverter.Infrastructure;

namespace FormatConverter.Convert.Reader
{
    public class FileReaderFactory
    {
        private static IEnumerable<FileReader> _readers = new List<FileReader>();

        public static FileReader GetReader(string path)
        {
            GetReaders();

            var extention = Path
                .GetExtension(path)
                .Replace(".", "")
                .ToLower();

            return _readers.SingleOrDefault(r => r.Extention == extention) ??
                   throw new FileTypeNotSupportedException("File type is not supported!");
        }

        private static void GetReaders()
        {
            if (_readers.Any())
                return;

            _readers = ServiceResolver.GetImplementationsOf<FileReader>();
        }
    }
}