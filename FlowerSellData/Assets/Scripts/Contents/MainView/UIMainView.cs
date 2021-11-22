using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KMH.UI
{
    public class UIMainView : MonoBehaviour
    {
        private enum FindType : int { tag, type, name, }

        [SerializeField] private List<UIButtonBase> buttons = new List<UIButtonBase>();

        [SerializeField] private UIFlowerScrollController controller = null;

        private void OnEnable()
        {
            Initialized();
        }

        private void Initialized()
        {
            buttons[(int)FindType.tag].AddClickAction(FindTag);
            buttons[(int)FindType.type].AddClickAction(FindFlowerType);
            buttons[(int)FindType.name].AddClickAction(FindFlowerName);

            controller.CreateItems();
        }

        private void FindTag()
        {
            Debug.LogError("[UIMainView][FindTag]");
        }

        private void FindFlowerType()
        {
            Debug.LogError("[UIMainView][FindFlowerType]");
        }

        private void FindFlowerName()
        {
            Debug.LogError("[UIMainView][FindFlowerName]");
        }
    }
}

