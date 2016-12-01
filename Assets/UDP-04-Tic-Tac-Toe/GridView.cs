using UnityEngine;
using UnityEngine.UI;

namespace Udp04
{
    [System.Serializable]
    public class GridView 
    {
        [SerializeField]
        private GridLayoutGroup m_GridPanel;
        private RectTransform m_GridTransform;

        public void Initialize(int dimension)
        {
            m_GridTransform = m_GridPanel.transform as RectTransform;

            m_GridPanel.childAlignment = TextAnchor.MiddleCenter;
            m_GridPanel.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            m_GridPanel.constraintCount = dimension;
        }

        public void Validate()
        {
            Vector2 rectSize = m_GridTransform.rect.size;

            float minSize = Mathf.Min(rectSize.x, rectSize.y);
            float eachSize = minSize / m_GridPanel.constraintCount;

            m_GridPanel.cellSize = new Vector2(eachSize, eachSize);
        }

        public Transform transform
        {
            get { return m_GridPanel.transform; }
        }

        public bool isActive
        {
            set { m_GridPanel.gameObject.SetActive(value); }
        }
    }

}