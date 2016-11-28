using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tcp01
{
    public class TcpServer : MonoBehaviour 
    {
        public int sourcePort = 6000;
        
        void OnEnable () 
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            EndPoint srcEndPoint = new IPEndPoint(IPAddress.Any, sourcePort);

            socket.Bind(srcEndPoint);
            socket.Listen(10);

            Socket client = socket.Accept();

            byte[] buffer = new byte[1024];
            int length = client.Receive(buffer);

            string msg = Encoding.UTF8.GetString(buffer, 0, length);

            Debug.Log(msg);

            client.Close();
            socket.Close();
        }
    }

}