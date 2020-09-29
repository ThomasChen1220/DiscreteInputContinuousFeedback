using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Roam))]
public class SlimeBehavior : MonoBehaviour
{
    //0 for slimeA, 1 for slimeB, 2 for slimeC
    public int mSlimeID = 0;
    public virtual void DoMyThing(Collision collision) { }

    private Roam mRoam;
    protected virtual void OnStart() {

    }
    private void Start()
    {
        OnStart();
        mRoam = gameObject.GetComponent<Roam>();
    }
}
