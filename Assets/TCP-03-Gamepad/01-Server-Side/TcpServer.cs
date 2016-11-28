using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;

using System;
using System.Threading;
using System.Collections.Generic;

namespace Tcp03.Server
{
    public class TcpServer : MonoBehaviour 
    {
        public int sourcePort = 6000;

        public Action<int> onConnected;
        public Action<int> onDisconnected;
        public Action<int, string> onReceived;

        private Socket m_Socket;
        private Thread m_AcceptThread;
        private bool m_AcceptRunning;

        private List<TcpChannel> m_Channels = new List<TcpChannel>();
        private Queue<int> m_Connections = new Queue<int>();

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

                lock (channel.receivedMessages)
                {
                    channel.receivedMessages.Enqueue("Connected");
                }

                m_Channels.Add(channel);
            }
        }

        void Update()
        {
            for (int i = 0; i < m_Channels.Count;)
            {
                TcpChannel channel = m_Channels[i];
                Queue<string> messages = channel.receivedMessages;

                bool isDisconnected = false;

                lock (messages)
                {
                    while (messages.Count > 0)
                    {
                        string msg = messages.Dequeue();

                        if (msg == "Connected")
                        {
                            if (null != onConnected)
                            {
                                onConnected.Invoke(i);
                            }
                        }
                        else if (msg == "Disconnected" || string.IsNullOrEmpty(msg.Trim()))
                        {
                            if (null != onDisconnected)
                            {
                                onDisconnected.Invoke(i);
                            }

                            m_Channels.RemoveAt(i);
                            isDisconnected = true;
                            break;
                        }
                        else
                        {
                            if (null != onReceived)
                            {
                                onReceived.Invoke(i, msg);
                            }
                        }
                    }
                }

                if (!isDisconnected)
                {
                    i++;
                }
            }
        }
    }

}