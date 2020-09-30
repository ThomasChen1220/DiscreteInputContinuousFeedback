using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Aggresive : SlimeBehavior
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void OnStart()
    {
        mSlimeID = 1;
    }

    public override void DoMyThing(Collision2D collision)
    {
        // Briefly increase size to indicate eating
        StartCoroutine(Eating());

        audioSource.Play();

        Destroy(collision.gameObject); // Destroy the other slime (regardless of what type)
    }

    IEnumerator Eating()
    {
        transform.localScale = new Vector3(2f, 2f, 2f);

        yield return new WaitForSeconds(0.5f);

        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
