using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;

using System.Threading;
using System.Collections.Generic;

namespace Tcp02
{
    public class TcpClient : MonoBehaviour 
    {
        public string destinationIP = "127.0.0.1";
        public int destinationPort  = 6000;

        private Socket m_Socket;

        void OnDisable()
        {
            if (null != m_Socket)
            {
                m_Socket.Shutdown(SocketShutdown.Both);
                m_Socket.Close();
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_Socket.Connect(destinationIP, destinationPort);
            }

            if(null != m_Socket && Input.GetKeyDown(KeyCode.Space))
            {
                byte[] buffer = Encoding.ASCII.GetBytes("Hello, World");
                m_Socket.Send(buffer);
            }
        }
    }
}