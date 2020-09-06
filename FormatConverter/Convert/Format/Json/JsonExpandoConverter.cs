using System.Text.Json;
using FormatConverter.Convert.Expando;

namespace FormatConverter.Convert.Format.Json
{
    public class JsonExpandoConverter : IExpandoConverter<string>
    {
        public string FileExtention => "json";

        public SerializableExpando ToExpando(string content)
        {
            return JsonSerializer.Deserialize<SerializableExpando>(content);
        }
    }
}