using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml.Schema;

namespace FormatConverter.Convert
{
    public static class FileNameExtention
    {
        public const string Json = "json";
        public const string Csv = "csv";
        public const string Xml = "xml";
    }

    public class SerializableExpando : DynamicObject, IDictionary<string, object>
    {
        protected readonly IDictionary<string, object> expando;

        public SerializableExpando()
        {
            expando = new ExpandoObject();
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void Add(string key, object value)
        {
            expando.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return expando.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return expando.Keys; }
        }

        public bool Remove(string key)
        {
            return expando.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return expando.TryGetValue(key, out value);
        }

        public ICollection<object> Values
        {
            get { return expando.Values; }
        }

        public object this[string key]
        {
            get
            {
                return expando[key];
            }
            set
            {
                expando[key] = value;
            }
        }

        public void Add(KeyValuePair<string, object> item)
        {
            expando.Add(item);
        }

        public void Clear()
        {
            expando.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return expando.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            expando.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return expando.Count; }
        }

        public bool IsReadOnly
        {
            get { return expando.IsReadOnly; }
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return expando.Remove(item);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return expando.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}