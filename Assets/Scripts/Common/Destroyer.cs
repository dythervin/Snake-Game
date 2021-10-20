using Sirenix.OdinInspector;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Destroyer : TagsContainer
{

    private void OnTriggerEnter(Collider other)
    {
        if (OtherHasTag(other))
        {
            Destroy(other.gameObject);
        }
    }

}
