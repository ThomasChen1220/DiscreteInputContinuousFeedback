﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Peaceful : SlimeBehavior
{
    public float mateInterval = 3f;
    public float lastMate;
    protected override void OnStart()
    {
        base.OnStart();
        lastMate = Time.time;
    }
    public override void DoMyThing(Collision2D collision) {
        B_Peaceful other = collision.gameObject.GetComponent<B_Peaceful>();
        if (other != null)
        {
            float nextMateTime = Mathf.Min(lastMate + mateInterval, other.lastMate + other.mateInterval);
            if(nextMateTime < Time.time && lastMate<other.lastMate)
            {
                //make a baby
                Debug.Log("Mating");
                GameObject c = Instantiate(gameObject);
                c.transform.position = (transform.position + other.transform.position)/2;
                lastMate = Time.time;
                other.lastMate = Time.time;
            }
        }
    }
}
