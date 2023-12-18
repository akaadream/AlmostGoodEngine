using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Serialization
{
    internal static class Binary
    {
        /// <summary>
        /// Write the instance content inside a file given by the filename
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="filename"></param>
        internal static async Task Write<T>(T instance, string filename)
        {
            BinaryFormatter formatter = new();

            using FileStream fileStream = new(filename, FileMode.Create);
            await Task.Run(() => formatter.Serialize(fileStream, instance));
        }

        internal static async Task<T> Read<T>(string filename)
        {
            BinaryFormatter formatter = new();
            using FileStream fileStream = new(filename, FileMode.Open);
            return await Task.Run(() => (T)formatter.Deserialize(fileStream));
        }
    }
}
