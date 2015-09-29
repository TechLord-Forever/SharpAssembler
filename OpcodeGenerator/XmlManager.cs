using System;
using System.IO;
using System.Xml.Serialization;

namespace OpcodeGenerator
{
    public static class XmlManager
    {
        public static T Load<T>(string path) where T : class
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found", path);

            using (var fstream = File.OpenRead(path))
            {
                var serialiser = new XmlSerializer(typeof(T));

                serialiser.UnknownAttribute += (o, e) => {
                    Console.WriteLine("Unknown attribute: {0} at line: {1} position: {2}",
                        e.Attr, e.LineNumber, e.LinePosition);
                };

                serialiser.UnknownElement += (o, e) => {
                    Console.WriteLine("Unknown Element: {0} at line: {1} position: {2}",
                        e.Element, e.LineNumber, e.LinePosition);
                };

                return (T)serialiser.Deserialize(fstream);
            }
        }

        public static void Save<T>(string path, T obj) where T : class
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            using (var fstream = File.Open(path, FileMode.Create))
            {
                var serialiser = new XmlSerializer(typeof(T));
                serialiser.Serialize(fstream, obj);
                fstream.Flush();
            }
        }
    }
}