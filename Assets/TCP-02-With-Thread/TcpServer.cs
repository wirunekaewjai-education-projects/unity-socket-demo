using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;

using System.Threading;
using System.Collections.Generic;

namespace Tcp02
{
    public class TcpServer : MonoBehaviour 
    {
        public int sourcePort = 6000;

        private Socket m_Socket;
        private Thread m_AcceptThread;
        private bool m_AcceptRunning;

        private List<TcpChannel> m_Channels = new List<TcpChannel>();

        void OnEnable () 
        {
            m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            EndPoint srcEndPoint = new IPEndPoint(IPAddress.Any, sourcePort);

            m_Socket.Bind(srcEndPoint);
            m_Socket.Listen(10);

            m_AcceptRunning = true;

            m_AcceptThread = new Thread(OnAccepting);
            m_AcceptThread.Start();
        }

        void OnDisable()
        {
            m_AcceptRunning = false;
            m_AcceptThread.Abort();

            foreach (var channel in m_Channels)
            {
                channel.Stop();
            }

            m_Socket.Close();
        }

        void OnAccepting()
        {
            while (m_AcceptRunning)
            {
                Socket client = m_Socket.Accept();
                TcpChannel channel = new TcpChannel(client);

                channel.Start();

                m_Channels.Add(channel);
            }
        }
    }

}