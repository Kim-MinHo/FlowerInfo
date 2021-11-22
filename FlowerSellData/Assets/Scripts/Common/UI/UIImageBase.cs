using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KMH.UI
{
    public class UIImageBase : MonoBehaviour
    {
        [SerializeField] protected Image image;

        public Image Image => image;

        private void Awake()
        {
            if (image == null)
                image = this.GetComponent<Image>();
        }

        public void ShowImage(bool isShow)
        {
            image.gameObject.SetActive(isShow);
        }

        public void SetAlpha(float alpha)
        {
            var originColor = image.color;
            originColor.a = alpha;

            image.color = originColor;
        }

        public void SetImage(string path)
        {
            var sprite = ResourcesManger.Instance.LoadSprite(path);
            image.sprite = sprite;
        }

        public void SetShow(bool isShow)
        {
            image.gameObject.SetActive(isShow);
        }
    }
}

