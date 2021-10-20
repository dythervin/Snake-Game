using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public static class TagsHelper 
{
    public static string[] Array => UnityEditorInternal.InternalEditorUtility.tags;

}
#endif
