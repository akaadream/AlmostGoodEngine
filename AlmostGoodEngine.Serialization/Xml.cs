using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AlmostGoodEngine.Serialization
{
    internal static class Xml
    {
        /// <summary>
        /// Write the instance content inside a file given by the filename
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="filename"></param>
        internal static async Task Write<T>(T instance, string filename)
        {
            XmlSerializer serializer = new(typeof(T));
            using TextWriter writer = new StreamWriter(filename);
            await Task.Run(() => serializer.Serialize(writer, instance));
        }

        internal static async Task<T> Read<T>(string filename)
        {
            XmlSerializer serializer = new(typeof(T));
            using TextReader reader = new StreamReader(filename);
            return await Task.Run(() => (T)serializer.Deserialize(reader));
        }
    }
}
