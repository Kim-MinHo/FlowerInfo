using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KMH
{
    public abstract class ExcelTable : ScriptableObject
    {
        public abstract void Initialized(List<Dictionary<string, object>> data);
    }
}


