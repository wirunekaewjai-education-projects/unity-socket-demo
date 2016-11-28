using UnityEngine;
using System.Collections;

namespace Tcp03.Client
{
    public class GamepadController : MonoBehaviour 
    {
        private TcpClient m_Client;
        private int[] m_Directions;

    	void Start () 
        {
            m_Client = FindObjectOfType<TcpClient>();
            m_Directions = new int[2];
    	}

        void Update()
        {
            if (m_Directions[0] < 0)
            {
                m_Client.Send("MoveLeft");
            }
            else if (m_Directions[0] > 0)
            {
                m_Client.Send("MoveRight");
            }

            if (m_Directions[1] < 0)
            {
                m_Client.Send("ClimbDown");
            }
            else if (m_Directions[1] > 0)
            {
                m_Client.Send("ClimbUp");
            }
        }

        public void MoveLeft()
        {
            m_Directions[0] = -1;
        }

        public void MoveRight()
        {
            m_Directions[0] = 1;
        }

        public void MoveNone()
        {
            m_Directions[0] = 0;
        }

        public void ClimbDown()
        {
            m_Directions[1] = -1;
        }

        public void ClimbUp()
        {
            m_Directions[1] = 1;
        }

        public void ClimbNone()
        {
            m_Directions[1] = 0;
        }
    }
}