using Newtonsoft.Json;
using System.IO;
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
            JsonSerializerSettings serializerSettings = new();
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;

#if DEBUG
            serializerSettings.Formatting = Formatting.Indented;
#else
            serializerSettings.Formatting = Formatting.None;
#endif
            string json = JsonConvert.SerializeObject(instance, serializerSettings);
            using var streamWriter = new StreamWriter(filename);
            await streamWriter.WriteAsync(json);
        }

        internal static async Task<T> Read<T>(string filename)
        {
            using StreamReader reader = new(filename);
            string json = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
