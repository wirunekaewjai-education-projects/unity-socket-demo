using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;

using System.Threading;

namespace Demo02
{
    public class UdpReceiver : MonoBehaviour 
    {
        public int sourcePort  = 6000;

        private Socket m_Socket;
        private Thread m_Thread;
        private bool m_Running;

        private void OnEnable()
        {
            m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            EndPoint srcEndPoint = new IPEndPoint(IPAddress.Any, sourcePort);
            m_Socket.Bind(srcEndPoint);

            m_Running = true;

            m_Thread = new Thread(OnRunning);
            m_Thread.Start();
        }

        private void OnDisable()
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
                int length = m_Socket.Receive(buffer);
                string msg = Encoding.UTF8.GetString(buffer, 0, length);

                Debug.Log(msg);
            }
        }
    }
}
