using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectExtentions 
{
    public static void Dirty(this Object obj, Object target)
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(target);
#endif
    }

    public static void Dirty(this Object obj)
    {
        obj.Dirty(obj);
    }
}
