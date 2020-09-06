using System.Collections.Generic;
using System.Linq;
using FormatConverter.Infrastructure;

namespace FormatConverter.Convert.Expando
{
    public class ExpandoConverterFactory
    {
        private static IEnumerable<IExpandoConverter> _converters = new List<IExpandoConverter>();

        public static IExpandoConverter GetConverter(string fileExtention)
        {
            GetConverters();

            return _converters.Single(c => c.FileExtention == fileExtention);
        }

        private static void GetConverters()
        {
            if(_converters.Any())
                return;

            _converters = ServiceResolver.GetImplementationsOf<IExpandoConverter>();
        }
    }
}