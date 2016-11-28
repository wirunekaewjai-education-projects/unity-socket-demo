using UnityEngine;

namespace Udp03.Part02
{
    public class GamepadControllerV2 : MonoBehaviour 
    {
        private UdpSender m_Sender;
        private int[] m_Directions;

        void Start()
        {
            m_Sender = FindObjectOfType<UdpSender>();
            m_Directions = new int[2];
        }

        void Update()
        {
            if (m_Directions[0] < 0)
            {
                m_Sender.Send("MoveLeft");
            }
            else if (m_Directions[0] > 0)
            {
                m_Sender.Send("MoveRight");
            }

            if (m_Directions[1] < 0)
            {
                m_Sender.Send("ClimbDown");
            }
            else if (m_Directions[1] > 0)
            {
                m_Sender.Send("ClimbUp");
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
