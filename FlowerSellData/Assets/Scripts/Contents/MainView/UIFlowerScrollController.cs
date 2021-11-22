using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KMH.UI
{
    public class UIFlowerScrollController : UIScrollBase<UIFlowerItem>
    {
        public void CreateItems()
        {
            var content = AddContent();

            content.flower.SetImage("");
            content.flowerName.SetText("");

            var count = 0;
            for (var i = 0; i < count; i++)
                content.tags[i].SetText("");

            content.hit.SetShow(true);
        }
    }
}


