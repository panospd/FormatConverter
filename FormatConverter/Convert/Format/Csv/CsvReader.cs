using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FormatConverter.Convert.Expando;
using FormatConverter.Convert.Reader;

namespace FormatConverter.Convert.Format.Csv
{
    public class CsvReader : FileReader<List<List<string>>>
    {
        public override string Extention => FileNameExtention.Csv;

        protected override List<List<string>> ReadFromFile(string path)
        {
            using var reader = new StreamReader(path);

            List<List<string>> lines = new List<List<string>>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if(line == null)
                    throw new ArgumentNullException(nameof(line));

                var values = line.Split(',').ToList();

                lines.Add(values);
            }

            return lines;
        }

        protected override void EnsureInputValid(List<List<string>> content)
        {
            if (!content.Any() || content.Count != 2 || content[0].Count != content[1].Count)
                throw new FormatException("CSV file is either empty or not in a correct format");
        }
    }
}
