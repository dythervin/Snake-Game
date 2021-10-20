using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnabler : TagsContainer
{
    [SerializeField] private bool disableOnStart = true;
    [SerializeField] private GameObject[] objects;
    [SerializeField] private Behaviour[] components;

    private void Start()
    {
        if (!disableOnStart)
            return;

        Enable(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OtherHasTag(other))
        {
            Enable(true);
            Destroy(this);
        }
    }

    private void Enable(bool value)
    {
        foreach (var obj in objects)
            obj.SetActive(value);

        foreach (var component in components)
            component.enabled = value;

    }
}
