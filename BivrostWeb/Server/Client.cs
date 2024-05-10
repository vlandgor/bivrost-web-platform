using System.Net.Sockets;

namespace BivrostWeb.Server;

public class Client
{
    private const int DATA_BUFFER_SIZE = 4096;

    public string id;
    public TCP tcp;

    public Client(string clientId)
    {
        id = clientId;
        tcp = new TCP(id);
    }

    public class TCP
    {
        public TcpClient socket;

        private readonly string id;
        private NetworkStream stream;
        private Packet receivedData;
        private byte[] receiveBuffer;

        public TCP(string id)
        {
            this.id = id;
        }

        public void Connect(TcpClient socket)
        {
            this.socket = socket;
            socket.ReceiveBufferSize = DATA_BUFFER_SIZE;
            socket.SendBufferSize = DATA_BUFFER_SIZE;

            stream = socket.GetStream();

            receivedData = new Packet();
            receiveBuffer = new byte[DATA_BUFFER_SIZE];

            stream.BeginRead(receiveBuffer, 0, DATA_BUFFER_SIZE, ReceiveCallback, null);
            
            ServerSend.Welcome(id, "Welcome to the server || lucidware.ca");
        }
        
        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                int byteLength = stream.EndRead(result);
                if (byteLength <= 0)
                {
                    // TODO: disconnect
                    return;
                }

                byte[] data = new byte[byteLength];
                Array.Copy(receiveBuffer, data, byteLength);
                
                receivedData.Reset(HandleData(data));
                stream.BeginRead(receiveBuffer, 0, DATA_BUFFER_SIZE, ReceiveCallback, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error receiving TCP data: {e}");
                // TODO: disconnect
            }
        }

        private bool HandleData(byte[] data)
        {
            int _packetLength = 0;
            receivedData.SetBytes(data);

            if (receivedData.UnreadLength() >= 4)
            {
                _packetLength = receivedData.ReadInt();
                if (_packetLength <= 0)
                {
                    return true;
                }
            }

            while (_packetLength > 0 && _packetLength <= receivedData.UnreadLength())
            {
                byte[] _packetBytes = receivedData.ReadBytes(_packetLength);
                
                // ThreadManager.ExecuteOnMainThread(() =>
                // {
                //     using (Packet _packet = new Packet(_packetBytes))
                //     {
                //         int _packetId = _packet.ReadInt();
                //         TcpListenerService.packetHandlers[_packetId](id, _packet);
                //     }
                // });

                _packetLength = 0;
                if (receivedData.UnreadLength() >= 4)
                {
                    _packetLength = receivedData.ReadInt();
                    if (_packetLength <= 0)
                    {
                        return true;
                    }
                }
            }

            if (_packetLength <= 1)
            {
                return true;
            }

            return false;
        }
        
        public void SendData(Packet packet)
        {
            try
            {
                stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error sending data to player {id} via TCP: {exception}");
            }
        }
    }
}