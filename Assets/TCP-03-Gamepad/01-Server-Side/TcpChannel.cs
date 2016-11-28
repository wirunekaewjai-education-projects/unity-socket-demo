using UnityEngine;

using System.Net.Sockets;
using System.Text;

using System.Threading;
using System.Collections.Generic;

namespace Tcp03.Server
{
    public class TcpChannel
    {
        private Socket m_Socket;
        private Thread m_Thread;
        private bool m_Running;

        public readonly Queue<string> receivedMessages = new Queue<string>();

        public TcpChannel(Socket socket)
        {
            m_Socket = socket;
        }

        public void Start()
        {
            m_Running = true;

            m_Thread = new Thread(OnRunning);
            m_Thread.Start();
        }

        public void Stop()
        {
            m_Socket.Close();

            m_Running = false;
            m_Thread.Abort();
        }

        private void OnRunning()
        {
            byte[] buffer = new byte[1024];

            while (m_Running)
            {
                try
                {
                    int length = m_Socket.Receive(buffer);
                    string msg = Encoding.ASCII.GetString(buffer, 0, length);

                    lock (receivedMessages)
                    {
                        receivedMessages.Enqueue(msg);
                    }
                }
                catch(SocketException)
                {
                    lock (receivedMessages)
                    {
                        receivedMessages.Enqueue("Disconnected");
                    }
                }
            }
        }

        public void Send(string msg)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(msg);
            m_Socket.Send(buffer);
        }
    }
}
