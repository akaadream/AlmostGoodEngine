using System;
using System.Text.Json;

namespace AlmostGoodEngine.Net
{
    [Serializable]
    public class Message
    {
        public string Type { get; set; }
        public string Content { get; set; }

        public Message(string type, string content)
        {
            Type = type;
            Content = content;
        }

        public byte[] Serialize()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this);
        }

        public static Message Deserialize(byte[] data)
        {
            return (Message)JsonSerializer.Deserialize(data, typeof(Message));
        }
    }
}
