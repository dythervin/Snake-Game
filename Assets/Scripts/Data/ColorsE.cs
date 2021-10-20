using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

public partial class Colors
{
    public const string GetDataListPath = nameof(Colors) + "." + nameof(Instance) + "." + nameof(GetDataList) + "()";

    public IEnumerable GetDataList()
    {
        HashSet<ValueDropdownItem> objects = new HashSet<ValueDropdownItem>();
        for (int i = 0; i < values.Length; i++)
        {
            var data = values[i];
            objects.Add(new ValueDropdownItem(data.name, i));
        }

        return objects;
    }

}

#endif
