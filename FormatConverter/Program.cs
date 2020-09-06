using System;
using System.IO;
using FormatConverter.Convert;
using FormatConverter.Convert.Output;
using FormatConverter.Convert.Reader;

namespace FormatConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var userContinue = true;

            while (userContinue)
            {
                try
                {
                    Console.WriteLine("Please enter the full path of the file you would like to parse:");

                    string path = Console.ReadLine();
                    var reading = GetReading(path);

                    Console.WriteLine("Specify the output type: Json(1) | Xml(2)");

                    var outputType = GetConverterType(Console.ReadLine());
                    var prettyOutput = GetResultOutput(reading, outputType);

                    Console.WriteLine(prettyOutput);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("File could not be found. Please make sure that the file exists and the path is correct!");
                    Console.WriteLine(e.Message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
                finally
                {
                    userContinue = Continue();
                }
            }
        }

        private static string GetResultOutput(SerializableExpando reading, OUtputConverterType oUtputType)
        {
            var jsonOutPutConverter = OutputConverterFactory.GetConverter(oUtputType);

            return jsonOutPutConverter.PrettySerialize(reading);
        }

        private static OUtputConverterType GetConverterType(string keyInput)
        {
            var parsed = int.TryParse(keyInput, out int key);

            if(!parsed)
                throw new ArgumentException("Invalid Input");

            switch (key)
            {
                case 1:
                    return OUtputConverterType.Json;
                case 2:
                    return OUtputConverterType.Xml;
                default:
                    throw new ArgumentOutOfRangeException("Output type not supported");
            }
        }

        private static SerializableExpando GetReading(string path)
        {
            var reader = FileReaderFactory.GetReader(path);
            return reader.ParseFile(path);
        }

        private static bool Continue()
        {
            Console.WriteLine("Do you want to have another go? Yes(y) | No (any key)");
            return Console.ReadLine() == "y";
        }
    }
}
