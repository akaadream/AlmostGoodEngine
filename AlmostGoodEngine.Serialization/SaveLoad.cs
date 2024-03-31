using System.Threading.Tasks;

namespace AlmostGoodEngine.Serialization
{
    public enum SaveLoadType
    {
        Json,
        Xml,
    }

    public static class SaveLoad
    {
        /// <summary>
        /// Save an object to a file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="filename"></param>
        /// <param name="type"></param>
        /// <param name="safe"></param>
        /// <returns></returns>
        public static async Task Save<T>(T instance, string filename, SaveLoadType type, bool safe = true)
        {
            if (safe)
            {
                await SafeSave(instance, filename, type);
            }
            else
            {
                await Write(instance, filename, type);
            }
        }

        private static async Task SafeSave<T>(T instance, string filename, SaveLoadType type)
        {
            try
            {
                await Write(instance, filename, type);
            }
            catch { }
        }

        private static async Task Write<T>(T instance, string filename, SaveLoadType type)
        {
            switch (type)
            {
                case SaveLoadType.Xml:
                    await Xml.Write(instance, filename);
                    break;
                case SaveLoadType.Json:
                default:
                    await Json.Write(instance, filename);
                    break;
            }
        }

        /// <summary>
        /// Load an object from a file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <param name="type"></param>
        /// <param name="safe"></param>
        /// <returns></returns>
        public static async ValueTask<T> Load<T>(string filename, SaveLoadType type, bool safe = true)
        {
            if (safe)
            {
                return await SafeLoad<T>(filename, type);
            }

            return await Read<T>(filename, type);
        }

        private static async ValueTask<T> SafeLoad<T>(string filename, SaveLoadType type)
        {
            try
            {
                return await Read<T>(filename, type);
            }
            catch { }

            return default;
        }

        private static async ValueTask<T> Read<T>(string filename, SaveLoadType type)
        {
            return type switch
            {
                SaveLoadType.Xml => await Xml.Read<T>(filename),
                _ => await Json.Read<T>(filename),
            };
        }
    }
}
