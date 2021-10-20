using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagsContainer : MonoBehaviour
{
#if UNITY_EDITOR
    [ValueDropdown("@" + nameof(TagsHelper) + "." + nameof(TagsHelper.Array), IsUniqueList = true)]
#endif
    [SerializeField] protected string[] tags;


    protected bool OtherHasTag(Component other)
    {
        foreach (string tag in tags)
        {
            if (!other.CompareTag(tag))
                continue;

            return true;
        }

        return false;
    }
}
