using Newtonsoft.Json;
using System.IO;

namespace AlmostGoodEngine.Serialization
{
    public static class Json
    {
        public static void Save<T>(T instance, string filename, bool safe = true)
        {
            if (safe)
            {
                SafeSave(instance, filename);
                return;
            }

            Write(instance, filename);
        }

        private static void SafeSave<T>(T instance, string filename)
        {
            try
            {
                Write(instance, filename);
            }
            catch { }
        }

        /// <summary>
        /// Write the instance content inside a file given by the filename
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="filename"></param>
        private static void Write<T>(T instance, string filename)
        {
            JsonSerializerSettings serializerSettings = new();
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;

#if DEBUG
            serializerSettings.Formatting = Formatting.Indented;
#else
            serializerSettings.Formatting = Formatting.None;
#endif
            JsonConvert.SerializeObject(instance, serializerSettings);
        }

        public static T Load<T>(string filename, bool safe = true)
        {
            if (safe)
            {
                return SafeLoad<T>(filename);
            }

            return Read<T>(filename);
        }

        private static T SafeLoad<T>(string filename)
        {
            try
            {
                return Read<T>(filename);
            }
            catch { }

            return default;
        }

        private static T Read<T>(string filename)
        {
            using StreamReader reader = new(filename);
            return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
        }
    }
}
