using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Serialization
{
    internal static class Json
    {
        /// <summary>
        /// Write the instance content inside a file given by the filename
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="filename"></param>
        internal static async Task Write<T>(T instance, string filename)
        {

            JsonSerializerOptions serializerOptions = new();

#if DEBUG
			serializerOptions.WriteIndented = true;
#else
            serializerOptions.WriteIntended = false;
#endif
			string json = JsonSerializer.Serialize(instance, serializerOptions);
            using var streamWriter = new StreamWriter(filename);
            await streamWriter.WriteAsync(json);
        }

        internal static async Task<T> Read<T>(string filename)
        {
            using StreamReader reader = new(filename);
            string json = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
