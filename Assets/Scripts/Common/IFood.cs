using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFood
{
    void OnMonthReached();
    void MoveToMouth(Transform mouth, in float speed);
    void MoveToMouth(Transform mouth, in float speed, in Vector3 offset);
}
