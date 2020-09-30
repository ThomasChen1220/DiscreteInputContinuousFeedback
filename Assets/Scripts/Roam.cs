using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SlimeBehavior))]
public class Roam : MonoBehaviour
{
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

        CalcNewDir();
        currTime = 0f;
        directionChangeTime = Random.Range(2f, 5f);

        // Add self to GM count
        gameManager.IncrementCount(mBehavior.mSlimeID);
    }

    // In this case, works same as with On Destroy
    private void OnDisable()
    {
        //Debug.Log("Roam: OnDisable called");
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

    // Made public so it can be called by behavior
    public void ChangeDir() {
        currTime = Time.time;
        CalcNewDir();
        directionChangeTime = Random.Range(2f, 5f);
    }

    void HandleCollision(Collision2D collision)
    {
        // If collided with wall, redecide a direction
        if (collision.gameObject.tag == "Wall")
        {
            ChangeDir();
        }

        // Interaction with other monsters: should communicate with GameManager to keep it updated
        if (collision.gameObject.tag == "Slime")
        {
            // Do slime behavior for appropriate type of slime
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

    // Used by blue slime C
    public void BoostSpeed()
    {
        StartCoroutine(IncreaseSpeed(1f)); // Boost speed for 1 second
    }

    // Increases speed for s seconds
    IEnumerator IncreaseSpeed(float s)
    {
        moveSpeed += 0.5f;

        yield return new WaitForSeconds(s);

        moveSpeed -= 0.5f;
    }
}
