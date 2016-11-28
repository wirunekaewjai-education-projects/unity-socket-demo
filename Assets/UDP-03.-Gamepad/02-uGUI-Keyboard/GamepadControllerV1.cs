using UnityEngine;

namespace Udp03.Part02
{
    public class GamepadControllerV1 : MonoBehaviour 
    {
        private UdpSender m_Sender;

        void Start()
        {
            m_Sender = FindObjectOfType<UdpSender>();
        }

        public void MoveLeft()
        {
            m_Sender.Send("MoveLeft");
        }

        public void MoveRight()
        {
            m_Sender.Send("MoveRight");
        }

        public void ClimbDown()
        {
            m_Sender.Send("ClimbDown");
        }

        public void ClimbUp()
        {
            m_Sender.Send("ClimbUp");
        }
    }
}
