using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour
{
    //0 for slimeA, 1 for slimeB, 2 for slimeC
    public int mSlimeID = 0;
    public virtual void DoMyThing() { }

    private Roam mRoam;
    private void Start()
    {
        mRoam = gameObject.GetComponent<Roam>();
    }
}
