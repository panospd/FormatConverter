using FormatConverter.Convert.Expando;

namespace FormatConverter.Convert.Reader
{
    public abstract class FileReader
    {
        public abstract SerializableExpando ParseFile (string path);
        public abstract string Extention { get; }
    }

    public abstract class FileReader<TContent> : FileReader
    {
        public override SerializableExpando ParseFile(string path)
        {
            var content = ReadFromFile(path);

            EnsureInputValid(content);

            var expandoConverter = (IExpandoConverter<TContent>)ExpandoConverterFactory.GetConverter(Extention);

            return expandoConverter.ToExpando(content);
        }

        protected abstract TContent ReadFromFile(string path);
        protected abstract void EnsureInputValid(TContent content);
    }
}