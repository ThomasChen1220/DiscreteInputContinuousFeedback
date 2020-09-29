using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SlimeBehavior))]
public class Roam : MonoBehaviour
{
    //[SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private GameManager gameManager;

    // Movement
    [SerializeField] private float moveSpeed = 1f;
    private Vector3 currDir;
    private float currTime;
    private float directionChangeTime;
    private SlimeBehavior mBehavior;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mBehavior = gameObject.GetComponent<SlimeBehavior>();

        //rb2d = GetComponent<Rigidbody2D>();
        CalcNewDir();
        currTime = 0f;
        directionChangeTime = Random.Range(2f, 5f);

        //add self to GM count
        gameManager.IncrementCount(mBehavior.mSlimeID);
    }
    private void OnDisable()
    {
        gameManager.DecrementCount(mBehavior.mSlimeID);
    }

    void Update()
    {
        // Change direction and figure out for how many seconds
        if (Time.time - currTime > directionChangeTime)
        {
            ChangeDir();
        }

        transform.position += currDir * moveSpeed * Time.deltaTime;
    }

    void ChangeDir() {
        currTime = Time.time;
        CalcNewDir();
        directionChangeTime = Random.Range(2f, 5f);
    }
    void HandleCollision(Collision2D collision)
    {
        // If collided with wall, redecide a direction
        if (collision.gameObject.tag == "Wall")
        {
            //Debug.Log("Change Dir");
            ChangeDir();
        }
        // Interaction with other monsters: should communicate with GameManager to keep it updated
        if (collision.gameObject.tag == "Slime")
        {
            //do the slime behavior
            mBehavior.DoMyThing(collision);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        HandleCollision(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision);
    }

    void CalcNewDir()
    {
        currDir = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0f).normalized;
    }


}
