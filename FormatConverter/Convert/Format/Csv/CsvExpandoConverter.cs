using System.Collections.Generic;
using FormatConverter.Convert.Expando;

namespace FormatConverter.Convert.Format.Csv
{
    public class CsvExpandoConverter : IExpandoConverter<List<List<string>>>
    {
        public string FileExtention => "csv";

        public SerializableExpando ToExpando(List<List<string>> content)
        {
            var properties = content[0];
            var values = content[1];

            var result = new SerializableExpando();

            for (int i = 0; i < properties.Count; i++)
            {
                var field = properties[i];

                if (!field.Contains("_"))
                {
                    result.Add(field, values[i]);
                    continue;
                }

                AddChildExpando(result, field, values[i]);
            }

            return result;
        }

        private static void AddChildExpando(SerializableExpando result, string complexField, string value)
        {
            var intendedFields = complexField.Split("_");

            var relativeParent = result;

            for (var j = 0; j < intendedFields.Length; j++)
            {
                var relativeParentFiend = intendedFields[j].Replace(" ", string.Empty);

                if (j == intendedFields.Length - 1)
                {
                    relativeParent.Add(relativeParentFiend, value);
                    continue;
                }

                if (relativeParent.ContainsKey(relativeParentFiend))
                {
                    relativeParent = (SerializableExpando)relativeParent[relativeParentFiend];
                    continue;
                }

                var parentValueObject = new SerializableExpando();
                relativeParent.Add(relativeParentFiend, parentValueObject);

                relativeParent = parentValueObject;
            }
        }
    }
}