using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Aggresive : SlimeBehavior
{
    protected override void OnStart()
    {
        mSlimeID = 1;
    }

    public override void DoMyThing(Collision2D collision)
    {

    }
}
