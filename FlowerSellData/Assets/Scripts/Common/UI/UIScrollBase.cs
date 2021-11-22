using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KMH
{
    public class UIScrollBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private RectTransform parent = null;
        [SerializeField] private GameObject prefab = null;

        [HideInInspector] public List<T> contents = new List<T>();

        protected int contentsCount = 0;

        private void Awake()
        {
            var childItems = parent.GetComponentsInChildren<T>();

            foreach (var child in childItems)
            {
                contents.Add(child);
                contentsCount++;
            }
        }

        public virtual T AddContent()
        {
            GameObject contentGameObject = null;
            T contentItem = null;

            if (contentsCount < contents.Count)
            {
                contentItem = contents[contentsCount];
                contentGameObject = contentItem.gameObject;
            }
            else
            {
                contentGameObject = Instantiate(prefab, parent);
                contentItem = contentGameObject.GetComponent<T>();
                contents.Add(contentItem);
            }

            contentGameObject.SetActive(true);
            var tr = contentGameObject.GetComponent<RectTransform>();
            tr.SetSiblingIndex(contentsCount);

            contentsCount++;

            return contentItem;
        }

        public virtual void RemoveAllItems()
        {
            var count = contents.Count;
            for (var i = 0; i < count; i++)
            {
                if (contents[i] != null)
                {
                    contents[i].gameObject.SetActive(false);
                }
                else
                {
                    contents.RemoveAt(i);
                }
            }
            contentsCount = 0;
        }
    }
}


