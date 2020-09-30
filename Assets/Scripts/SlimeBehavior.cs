using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Roam))]
public class SlimeBehavior : MonoBehaviour
{
    // Base class for B_Aggressive, Peaceful, and Runaway

    // 0 for slimeA, 1 for slimeB, 2 for slimeC
    public int mSlimeID = 0;
    protected Roam mRoam;

    public virtual void DoMyThing(Collision2D collision) { }

    protected virtual void OnStart() { }

    private void Start()
    {
        OnStart();
        mRoam = gameObject.GetComponent<Roam>();
    }
}
