using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tcp01
{
    public class TcpClient : MonoBehaviour 
    {
        public string destinationIP = "127.0.0.1";
        public int destinationPort  = 6000;

        void OnEnable () 
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(destinationIP, destinationPort);

            byte[] buffer = Encoding.UTF8.GetBytes("Hello, World");
            socket.Send(buffer);

            socket.Disconnect(false);
            socket.Close();
        }
    }

}