using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingZ : Floating
{
    protected override void Awake()
    {
        offset = transform.position.z;
        base.Awake();

    }
}
