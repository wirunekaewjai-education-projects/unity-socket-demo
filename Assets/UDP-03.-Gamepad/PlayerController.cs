using UnityEngine;
using System.Collections;

namespace Demo03
{
    public class PlayerController : MonoBehaviour 
    {
        [SerializeField]
        private PlayerModel m_Model;

        private UdpReceiver m_Receiver;

        private void Start()
        {
            m_Receiver = FindObjectOfType<UdpReceiver>();
            m_Receiver.onReceived = OnReceived;
        }

        private void OnReceived(string msg)
        {
            if (msg == "MoveLeft")
            {
                m_Model.Move(-1);
            }
            else if (msg == "MoveRight")
            {
                m_Model.Move(1);
            }
            else if (msg == "ClimbDown")
            {
                m_Model.Climb(-1);
            }
            else if (msg == "ClimbUp")
            {
                m_Model.Climb(1);
            }
        }
    }
}