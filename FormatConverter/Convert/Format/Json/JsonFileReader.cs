using System;
using System.IO;
using FormatConverter.Convert.Reader;

namespace FormatConverter.Convert.Format.Json
{
    public class JsonFileReader : FileReader<string>
    {
        public override string Extention => FileNameExtention.Json;
        protected override string ReadFromFile(string path)
        {
            using StreamReader r = new StreamReader(path);
            return r.ReadToEnd();
        }

        protected override void EnsureInputValid(string content)
        {
            if(string.IsNullOrWhiteSpace(content))
                throw new FormatException("Json file is empty");
        }
    }
}