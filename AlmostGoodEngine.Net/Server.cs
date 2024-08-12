using Lidgren.Network;

namespace AlmostGoodEngine.Net
{
    public class Server
    {
        /// <summary>
        /// Server instance
        /// </summary>
        private NetServer _instance;

        /// <summary>
        /// Server configuration
        /// </summary>
        private NetPeerConfiguration _configuration;

        public Server(string id, int port)
        {
            // Server configuration
            _configuration = new(id)
            {
                Port = port
            };
            _configuration.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);

            _instance = new(_configuration);
            _instance.Start();
        }

        /// <summary>
        /// Listen messages from clients
        /// </summary>
        public virtual void Update()
        {
            NetIncomingMessage message;
            while ((message = _instance.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        HandleDiscoveryRequestMessage(message);
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChangedMessage(message);
                        break;
                    case NetIncomingMessageType.Data:
                        HandleDataMessage(Message.Deserialize(message.ReadBytes(message.LengthBytes)));
                        break;
                    default:
                        HandleOtherMessage(message);
                        break;
                }

                _instance.Recycle(message);
            }
        }

        /// <summary>
        /// Handle discovery request message (when a client connect to the server)
        /// </summary>
        /// <param name="message"></param>
        protected virtual void HandleDiscoveryRequestMessage(NetIncomingMessage message)
        {
            NetOutgoingMessage response = _instance.CreateMessage();
            response.Write("Connected to the server");
            _instance.SendDiscoveryResponse(response, message.SenderEndPoint);
        }

        /// <summary>
        /// Handle status changed message (when a client connection changed)
        /// </summary>
        /// <param name="message"></param>
        protected virtual void HandleStatusChangedMessage(NetIncomingMessage message)
        {
            var status = (NetConnectionStatus)message.ReadByte();
        }

        /// <summary>
        /// Handle data message (when a client send data to the server)
        /// </summary>
        /// <param name="message"></param>
        protected virtual void HandleDataMessage(Message message)
        {
            if (message == null)
            {
                return;
            }
        }

        protected virtual void HandleOtherMessage(NetIncomingMessage message)
        {

        }

        /// <summary>
        /// Send a message to a client
        /// </summary>
        /// <param name="content"></param>
        /// <param name="recipient"></param>
        public void SendMessage(Message message, NetConnection recipient)
        {
            NetOutgoingMessage netMessage = _instance.CreateMessage();
            netMessage.Write(message.Serialize());
            _instance.SendMessage(netMessage, recipient, NetDeliveryMethod.ReliableOrdered);
        }

        /// <summary>
        /// Server shutdown
        /// </summary>
        public virtual void Shutdown()
        {
            _instance.Shutdown("Server shutting down...");
        }
    }
}
