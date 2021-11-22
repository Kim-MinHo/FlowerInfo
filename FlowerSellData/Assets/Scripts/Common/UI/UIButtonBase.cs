using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace KMH
{
    public class UIButtonBase : MonoBehaviour
    {
        [SerializeField] protected Button button = null;
        [SerializeField] protected Text text = null;
        public Button Button => button;

        [SerializeField] protected UnityAction onClickAction;

        private void OnEnable()
        {
            if (button == null)
            {
                button = this.GetComponent<Button>();
                text = button.GetComponentInChildren<Text>();
            }
        }

        private void OnDisable()
        {
            RemoveAllClickAction();
        }

        public virtual void ClearButtonName()
        {
            text.text = "";
        }

        public virtual void SetButtonName(string name)
        {
            text.text = name;
        }

        public virtual void AddClickAction(UnityAction action)
        {
            onClickAction += action;
        }

        public virtual void RemoveClickAction(UnityAction action)
        {
            onClickAction -= action;
        }

        public virtual void RemoveAllClickAction()
        {
            onClickAction = null;
        }

        public virtual void OnClickButton()
        {
            onClickAction?.Invoke();
        }

        public virtual void SetInteractive(bool isActive)
        {
            if (button == null)
                return;

            button.interactable = isActive;
        }
        public virtual void SetActive(bool isActive)
        {
            button.gameObject.SetActive(isActive);
        }
    }
}



