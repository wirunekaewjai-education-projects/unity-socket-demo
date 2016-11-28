using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;

using System;
using System.Threading;
using System.Collections.Generic;

namespace Tcp03.Client
{
    public class TcpClient : MonoBehaviour 
    {
        public int destinationPort  = 6000;

        private string m_DestinationIP = "127.0.0.1";
        private Socket m_Socket;

        void OnDisable()
        {
            if (null != m_Socket)
            {
                Send("Disconnected");

                m_Socket.Shutdown(SocketShutdown.Both);
                m_Socket.Close();
            }
        }

        public string destinationIP
        {
            get { return m_DestinationIP; }
            set { m_DestinationIP = value; }
        }

        public void Connect()
        {
            m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_Socket.Connect(m_DestinationIP, destinationPort);
        }

        public void Send(string msg)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(msg);
            m_Socket.Send(buffer);
        }
    }
}