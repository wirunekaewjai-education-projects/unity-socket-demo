using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Udp01
{
    public class UdpSender : MonoBehaviour 
    {
        public string destinationIP = "127.0.0.1";
        public int destinationPort  = 6000;

        private void OnEnable()
        {
            IPAddress destAddress = IPAddress.Parse(destinationIP);
            EndPoint destEndPoint = new IPEndPoint(destAddress, destinationPort);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            byte[] buffer = Encoding.UTF8.GetBytes("Hello, World");
            socket.SendTo(buffer, destEndPoint);

            socket.Close();
        }
    }

}