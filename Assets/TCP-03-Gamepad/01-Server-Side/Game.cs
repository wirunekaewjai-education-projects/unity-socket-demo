using UnityEngine;
using System.Collections.Generic;

namespace Tcp03.Server
{
    public class Game : MonoBehaviour 
    {
        public Player prefab;

        private List<Player> m_Players = new List<Player>();
        private TcpServer m_Server;

        private void Start()
        {
            m_Server = FindObjectOfType<TcpServer>();
            m_Server.onConnected = OnConnected;
            m_Server.onDisconnected = OnDisconnected;
            m_Server.onReceived = OnReceived;
        }

        private void OnReceived(int index, string msg)
        {
//            if (index >= m_Players.Count)
//            {
//                Spawn();
//            }

            Player player = m_Players[index];

            if (msg == "MoveLeft")
            {
                player.Move(-1);
            }
            else if (msg == "MoveRight")
            {
                player.Move(1);
            }
            else if (msg == "ClimbDown")
            {
                player.Climb(-1);
            }
            else if (msg == "ClimbUp")
            {
                player.Climb(1);
            }
        }

        public void OnConnected(int index)
        {
            Player clone = Instantiate<Player>(prefab);

            float r = Random.Range(0.25f, 0.75f);
            float g = Random.Range(0.25f, 0.75f);
            float b = Random.Range(0.25f, 0.75f);

            clone.SetColor(new Color(r, g, b));

            m_Players.Add(clone);
        }

        public void OnDisconnected(int index)
        {
            Player player = m_Players[index];

            m_Players.RemoveAt(index);
            DestroyObject(player.gameObject);
        }
    }
}