using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Runaway : SlimeBehavior
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mRoam = gameObject.GetComponent<Roam>(); // Do this just in case
    }

    protected override void OnStart()
    {
        mSlimeID = 2;
    }

    public override void DoMyThing(Collision2D collision)
    {
        audioSource.Play();
        mRoam.ChangeDir();
        mRoam.BoostSpeed();
    }
}

