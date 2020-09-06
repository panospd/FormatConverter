namespace FormatConverter.Convert.Output
{
    public abstract class OutputConverter
    {
        public abstract OUtputConverterType Type { get; }

        public abstract string PrettySerialize(SerializableExpando output);
    }
}