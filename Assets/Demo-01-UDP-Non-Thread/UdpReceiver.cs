using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Demo01
{
    public class UdpReceiver : MonoBehaviour 
    {
        public int sourcePort  = 6000;

        private void OnEnable()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            EndPoint srcEndPoint = new IPEndPoint(IPAddress.Any, sourcePort);
            socket.Bind(srcEndPoint);

            byte[] buffer = new byte[1024];
            int length = socket.Receive(buffer);

            string msg = Encoding.UTF8.GetString(buffer, 0, length);

            Debug.Log(msg);

            socket.Close();
        }
    }
}
