using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Udp04
{
    public class GameLauncher : MonoBehaviour 
    {
        public int gameSceneIndex;

        public InputField dstIP;
        public InputField dstPort;
        public InputField srcPort;

        public void OnStartClick()
        {
            PlayerPrefs.SetString("DstIP", dstIP.text);
            PlayerPrefs.SetInt("DstPort", int.Parse(dstPort.text));
            PlayerPrefs.SetInt("SrcPort", int.Parse(srcPort.text));

            SceneManager.LoadScene(gameSceneIndex);
        }
    }

}