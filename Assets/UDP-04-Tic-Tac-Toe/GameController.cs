using UnityEngine;
using UnityEngine.SceneManagement;

namespace Udp04
{
    public class GameController : MonoBehaviour 
    {
        [Header("Options")]
        public int sceneIndex = 1;

        [Range(3, 9)]
        public int dimension = 3;

        public Sprite sprite;
        public Font font;

        [SerializeField]
        private GridView m_GridView;

        [SerializeField]
        private ResultView m_ResultView;

        private SlotView[,] m_Slots;

        private UdpSender m_Sender;
        private UdpReceiver m_Receiver;

        private bool m_IsX;
        private int m_TurnCount;
        private int m_MaxTurn;

        private void Start()
        {
            m_IsX = true;
            m_MaxTurn = dimension * dimension;

            m_Sender = FindObjectOfType<UdpSender>();
            m_Receiver = FindObjectOfType<UdpReceiver>();
            m_Receiver.onReceived = OnReceived;

            m_Sender.SetDestination(PlayerPrefs.GetString("DstIP"), PlayerPrefs.GetInt("DstPort"));
            m_Receiver.Listen(PlayerPrefs.GetInt("SrcPort"));

            m_GridView.Initialize(dimension);

            m_Slots = new SlotView[dimension, dimension];
            for (int y = 0; y < dimension; y++)
            {
                for (int x = 0; x < dimension; x++)
                {
                    m_Slots[x, y] = new SlotView(sprite, font, m_GridView.transform, x, y);
                    m_Slots[x, y].onClick = OnClick;
                }
            }
        }

        private void LateUpdate()
        {
            m_GridView.Validate();
        }

        private void OnReceived(string msg)
        {
            if (m_TurnCount == 0)
            {
                m_IsX = false;   
            }

            int x = int.Parse(msg[0] + "");
            int y = int.Parse(msg[1] + "");

            if (m_TurnCount >= m_MaxTurn)
                return;

            bool isZero = (m_TurnCount % 2 == 0);

//            if (isZero && !m_IsX)
//                return false;

            m_Slots[x, y].text = isZero ? "X" : "O";
            m_Slots[x, y].clickable = false;
            m_TurnCount += 1;

            SetResult();
        }

        private bool OnClick(int x, int y)
        {
            if (SetSlot(x, y))
            {
                m_Sender.Send(x + "" + y);
                SetResult();

                return true;
            }

            return false;
        }

        private bool SetSlot(int x, int y)
        {
            if (m_TurnCount >= m_MaxTurn)
                return false;

            bool isZero = (m_TurnCount % 2 == 0);

            if (!isZero && m_IsX)
                return false;

            if (isZero && !m_IsX)
                return false;

            m_Slots[x, y].text = isZero ? "X" : "O";
            m_TurnCount += 1;

            return true;
        }

        private void SetResult()
        {
            string result = GameLogic.GetResult(m_Slots, dimension);

            if(!string.IsNullOrEmpty(result))
            {
                ShowResult(result + " WIN !!!");
            }
            else if(m_TurnCount >= m_MaxTurn)
            {
                ShowResult("DRAWWW !!!");
            }
        }

        private void ShowResult(string text)
        {
            m_ResultView.text = text;

            for (int y = 0; y < dimension; y++)
            {
                for (int x = 0; x < dimension; x++)
                {
                    m_Slots[x, y].clickable = false;
                }
            }

            Invoke("OnShowResult", 1.5f);
        }

        private void OnShowResult()
        {
            m_GridView.isActive = false;
            m_ResultView.isActive = true;

            Invoke("OnRestart", 3);
        }

        private void OnRestart()
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

}