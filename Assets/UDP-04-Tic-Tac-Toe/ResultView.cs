using UnityEngine;
using UnityEngine.UI;

namespace Udp04
{
    [System.Serializable]
    public class ResultView 
    {
        [SerializeField]
        private GameObject m_ResultPanel;

        public string text
        {
            set
            {
                Transform child = m_ResultPanel.transform.GetChild(0);
                Text resultText = child.GetComponent<Text>();
                resultText.text = value;
            }
        }

        public bool isActive
        {
            set
            {
                m_ResultPanel.SetActive(value);
            }
        }
    }

}