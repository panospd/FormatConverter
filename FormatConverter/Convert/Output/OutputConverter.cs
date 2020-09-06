namespace FormatConverter.Convert.Output
{
    public abstract class OutputConverter
    {
        public abstract OutputConverterType Type { get; }

        public abstract string PrettySerialize(SerializableExpando output);
    }
}