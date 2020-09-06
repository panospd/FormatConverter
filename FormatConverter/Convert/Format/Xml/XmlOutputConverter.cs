using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using FormatConverter.Convert.Output;

namespace FormatConverter.Convert.Format.Xml
{
    public class XmlOutputConverter : OutputConverter
    {
        public override OUtputConverterType Type => OUtputConverterType.Xml;
        public override string PrettySerialize(SerializableExpando output)
        {
            var xmlSerializable = XmlSerializableExpando.CreateFrom(output);

            using var sww = new StringWriter();

            XmlWriterSettings settings = new XmlWriterSettings
            {
                ConformanceLevel = ConformanceLevel.Auto
            };

            XmlWriter writer = XmlWriter.Create(sww, settings);
            XmlSerializer serializer = new XmlSerializer(xmlSerializable.GetType());

            serializer.Serialize(writer, xmlSerializable);

            return PrintXml(sww.ToString());
        }

        public static string PrintXml(string xml)
        {
            XDocument doc = XDocument.Parse(xml);
            return doc.ToString();
        }
    }
}