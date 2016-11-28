using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Demo02
{
    public class UdpSender : MonoBehaviour 
    {
        public string destinationIP = "127.0.0.1";
        public int destinationPort  = 6000;

        private Socket m_Socket;
        private EndPoint m_DestEndPoint;

        private void OnEnable()
        {
            IPAddress destAddress = IPAddress.Parse(destinationIP);
            m_DestEndPoint = new IPEndPoint(destAddress, destinationPort);

            m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        private void OnDisable()
        {
            m_Socket.Close();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                byte[] buffer = Encoding.UTF8.GetBytes("Hello, World");
                m_Socket.SendTo(buffer, m_DestEndPoint);
            }
        }
    }

}