using UnityEngine;
using System.Collections;

namespace Udp03
{
    [System.Serializable]
    public class PlayerModel 
    {
        [SerializeField]
        private float m_MoveSpeed = 2;

        [SerializeField]
        private Rigidbody2D m_Physics;

        public void Move(float direction)
        {
            Vector2 p = new Vector2(direction * m_MoveSpeed * Time.deltaTime, 0);
            m_Physics.position = m_Physics.position + p;
        }

        public void Climb(float direction)
        {
            Vector2 p = new Vector2(0, direction * m_MoveSpeed * Time.deltaTime);
            m_Physics.position = m_Physics.position + p;
        }
    }
}
