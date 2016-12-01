﻿using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Udp04
{
    public class UdpSender : MonoBehaviour 
    {
//        public string destinationIP = "127.0.0.1";
//        public int destinationPort  = 6000;

        private Socket m_Socket;
        private EndPoint m_DestEndPoint;

        public void SetDestination(string ip, int port)
        {
            IPAddress destAddress = IPAddress.Parse(ip);
            m_DestEndPoint = new IPEndPoint(destAddress, port);

            m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        private void OnDisable()
        {
            m_Socket.Close();
        }

        public void Send(string msg)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            m_Socket.SendTo(buffer, m_DestEndPoint);
        }
    }

}