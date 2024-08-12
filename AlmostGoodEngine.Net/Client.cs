using Lidgren.Network;

namespace AlmostGoodEngine.Net
{
    public class Client
    {
        /// <summary>
        /// The client instance
        /// </summary>
        private NetClient _instance;

        /// <summary>
        /// The client connection configuration
        /// </summary>
        private NetPeerConfiguration _configuration;

        /// <summary>
        /// The connection link to the server
        /// </summary>
        private NetConnection _connection;

        public Client(string id, string address, int port)
        {
            _configuration = new("");

            _instance = new(_configuration);
            _instance.Start();

            var hailMessage = _instance.CreateMessage("Hello server!");
            _instance.Connect(address, port, hailMessage);
        }

        /// <summary>
        /// Listen messages from the server
        /// </summary>
        public virtual void Update()
        {
            NetIncomingMessage message;
            while ((message = _instance.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChangedMessage(message);
                        break;
                    case NetIncomingMessageType.Data:
                        HandleDataMessage(Message.Deserialize(message.ReadBytes(message.LengthBytes)));
                        break;
                    case NetIncomingMessageType.DiscoveryResponse:
                        HandleDiscoveryResponseMessage(message);
                        break;
                    default:
                        HandleOtherMessage(message);
                        break;
                }

                _instance.Recycle(message);
            }
        }

        /// <summary>
        /// Handle status changed message
        /// </summary>
        /// <param name="message"></param>
        protected virtual void HandleStatusChangedMessage(NetIncomingMessage message)
        {
            var status = (NetConnectionStatus)message.ReadByte();
            if (status == NetConnectionStatus.Connected)
            {
                // Successfully connected to the server
                _connection = message.SenderConnection;
            }
        }

        /// <summary>
        /// Handle discovery response message
        /// </summary>
        /// <param name="message"></param>
        protected virtual void HandleDiscoveryResponseMessage(NetIncomingMessage message)
        {
            string discoveryResponse = message.ReadString();
        }

        /// <summary>
        /// Handle data message
        /// </summary>
        /// <param name="message"></param>
        protected virtual void HandleDataMessage(Message message)
        {
            if (message == null)
            {
                return;
            }
        }

        /// <summary>
        /// Handle other message
        /// </summary>
        /// <param name="message"></param>
        protected virtual void HandleOtherMessage(NetIncomingMessage message)
        {
        }

        /// <summary>
        /// Send a message to the server
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(Message message)
        {
            if (_connection == null)
            {
                return;
            }

            NetOutgoingMessage netMessage = _instance.CreateMessage();
            netMessage.Write(message.Serialize());
            _instance.SendMessage(netMessage, _connection, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
