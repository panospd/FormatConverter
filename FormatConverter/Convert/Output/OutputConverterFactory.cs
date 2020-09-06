using System.Collections.Generic;
using System.Linq;
using FormatConverter.Infrastructure;

namespace FormatConverter.Convert.Output
{
    public class OutputConverterFactory
    {
        private static IEnumerable<OutputConverter> _converters = new List<OutputConverter>();

        public static OutputConverter GetConverter(OUtputConverterType type)
        {
            GetConverters();

            return _converters.Single(a => a.Type == type);
        }

        private static void GetConverters()
        {
            if(_converters.Any())
                return;

            _converters = ServiceResolver.GetImplementationsOf<OutputConverter>();
        }
    }
}