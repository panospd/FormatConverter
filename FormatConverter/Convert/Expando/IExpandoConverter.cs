namespace FormatConverter.Convert.Expando
{
    public interface IExpandoConverter
    {
        string FileExtention { get; }
    }

    public interface IExpandoConverter<in TInput> : IExpandoConverter
    {
        SerializableExpando ToExpando(TInput content);
    }
}