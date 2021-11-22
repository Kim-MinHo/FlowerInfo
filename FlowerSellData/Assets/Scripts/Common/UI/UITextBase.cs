using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KMH.UI
{
    public class UITextBase : MonoBehaviour
    {
        [SerializeField] protected Text text = null;


        private void Awake()
        {
            if (text == null)
                text = this.GetComponent<Text>();
        }

        public void SetTextClear()
        {
            text.text = "";
        }

        public void SetText(string text)
        {
            this.text.text = text;
        }

        public void SetFontSize(int size)
        {
            text.fontSize = size;
        }
    }
}


