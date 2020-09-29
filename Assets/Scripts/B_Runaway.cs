using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Runaway : SlimeBehavior
{
    protected override void OnStart()
    {
        mSlimeID = 2;
    }
    public override void DoMyThing(Collision collision)
    {

    }
}
