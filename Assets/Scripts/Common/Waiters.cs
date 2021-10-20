using UnityEngine;

public static class Waiters
{
    public readonly static WaitForFixedUpdate FixedUpdate = new WaitForFixedUpdate();
    public readonly static WaitForSeconds Second = new WaitForSeconds(1);
    public readonly static WaitForEndOfFrame EndOfFrame = new WaitForEndOfFrame();
}

