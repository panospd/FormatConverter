using System.Text.Json;
using FormatConverter.Convert.Output;

namespace FormatConverter.Convert.Format.Json
{
    public class JsonOutPutConverter : OutputConverter
    {
        public override OUtputConverterType Type => OUtputConverterType.Json;

        public override string PrettySerialize(SerializableExpando output)
        {
            if (output == null)
                return "{}";

            return Pretty(JsonSerializer.Serialize(output));
        }

        private static string Pretty(string json)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreNullValues = true
            };

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);

            return JsonSerializer.Serialize(jsonElement, options);
        }
    }
}


