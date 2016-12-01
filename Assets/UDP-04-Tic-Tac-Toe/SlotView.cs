using UnityEngine;
using UnityEngine.UI;

using System;

namespace Udp04
{
    public class SlotView 
    {
        private Button m_Button;
        private Text m_Text;

        private int m_X, m_Y;

        public Func<int, int, bool> onClick;

        public SlotView(Sprite sprite, Font font, Transform parent, int x, int y)
        {
            m_X = x;
            m_Y = y;

            SetupButton(sprite, parent);
            SetupText(font);
        }

        private void SetupButton(Sprite sprite, Transform parent)
        {
            GameObject obj = new GameObject("Button [" + m_X + ", " + m_Y + "]");
            m_Button = obj.AddComponent<Button>();
//            button.onClick.AddListener(OnClick);

            Image image = obj.AddComponent<Image>();
            image.type = Image.Type.Sliced;
            image.sprite = sprite;

            m_Button.targetGraphic = image;

            obj.layer = LayerMask.NameToLayer("UI");

            m_Button.transform.SetParent(parent);
            m_Button.transform.localScale = Vector3.one;
            m_Button.onClick.AddListener(() =>
                {
                    if(null != onClick)
                    {
                        if(onClick.Invoke(m_X, m_Y))
                        {
                            m_Button.interactable = false;
                        }
                    }
                });
        }

        private void SetupText(Font font)
        {
            GameObject obj = new GameObject("Text");
            m_Text = obj.AddComponent<Text>();
            m_Text.transform.SetParent(m_Button.transform);
            m_Text.font = font;
            m_Text.fontSize = 20;
            m_Text.fontStyle = FontStyle.Bold;
            m_Text.color = Color.black;
            m_Text.resizeTextForBestFit = true;
            m_Text.resizeTextMaxSize = 120;
            m_Text.text = string.Empty;

            ContentSizeFitter fitter = obj.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            RectTransform rt = m_Text.transform as RectTransform;
            rt.anchoredPosition = Vector2.zero;
            rt.sizeDelta = Vector2.zero;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
        }

        public string text
        {
            get { return m_Text.text; }
            set { m_Text.text = value; }
        }

        public bool clickable
        {
            get { return m_Button.interactable; }
            set { m_Button.interactable = value; }
        }
    }

}