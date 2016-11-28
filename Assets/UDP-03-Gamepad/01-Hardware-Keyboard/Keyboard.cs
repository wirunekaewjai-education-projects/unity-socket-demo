using UnityEngine;
using System.Collections;

namespace Udp03.Part01
{
    public class Keyboard : MonoBehaviour 
    {
        private UdpSender m_Sender;

        void Start()
        {
            m_Sender = FindObjectOfType<UdpSender>();
        }

        void Update () 
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            if (h < 0)
            {
                m_Sender.Send("MoveLeft");
            }
            else if (h > 0)
            {
                m_Sender.Send("MoveRight");
            }

            if (v < 0)
            {
                m_Sender.Send("ClimbDown");
            }
            else if (v > 0)
            {
                m_Sender.Send("ClimbUp");
            }
        }
    }
}
