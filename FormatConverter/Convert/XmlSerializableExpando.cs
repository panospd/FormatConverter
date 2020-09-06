using System;
using System.Xml;
using System.Xml.Serialization;

namespace FormatConverter.Convert
{
    public class XmlSerializableExpando : SerializableExpando, IXmlSerializable
    {
        private readonly string root = "XmlSerializableExpando";

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement(root);

            while (!reader.Name.Equals(root))
            {
                var name = reader.Name;

                reader.MoveToAttribute("type");

                var typeContent = reader.ReadContentAsString();
                var underlyingType = Type.GetType(typeContent);

                reader.MoveToContent();
                expando[name] = reader.ReadElementContentAs(underlyingType, null);
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (var key in expando.Keys)
            {
                var value = expando[key];

                writer.WriteStartElement(key);
                writer.WriteAttributeString("type", value.GetType().AssemblyQualifiedName);

                if (value is XmlSerializableExpando expandoObject)
                {
                    expandoObject.WriteXml(writer);
                    writer.WriteEndElement();
                    continue;
                }

                writer.WriteString(value.ToString());
                writer.WriteEndElement();
            }
        }

        public static XmlSerializableExpando CreateFrom(SerializableExpando serializableExpando)
        {
            var xmlSerializable = new XmlSerializableExpando();

            foreach (var (key, value) in serializableExpando)
            {
                if (value is SerializableExpando serializable)
                {
                    xmlSerializable.Add(key, CreateFrom(serializable));
                    continue;
                }   

                xmlSerializable.Add(key, value);
            }

            return xmlSerializable;
        }
    }
}