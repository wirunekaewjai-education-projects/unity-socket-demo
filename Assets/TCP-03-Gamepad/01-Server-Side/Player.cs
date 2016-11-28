using UnityEngine;
using System.Collections;

namespace Tcp03.Server
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float m_MoveSpeed = 2;

        [SerializeField]
        private Rigidbody2D m_Physics;

        [SerializeField]
        private SpriteRenderer m_Renderer;

        public void SetColor(Color color)
        {
            m_Renderer.color = color;
        }

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
