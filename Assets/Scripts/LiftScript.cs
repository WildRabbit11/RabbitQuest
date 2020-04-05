using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftScript : MonoBehaviour
{
    private Vector2 posA;//starting position
    private Vector2 posB;//second position
    private Vector2 nextPos;
    private bool movingTowards = false;
    [SerializeField] private float speed;
    [SerializeField] private Transform lift;
    [SerializeField] private Transform transformB;
    private Rigidbody2D rbody;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        posA = lift.localPosition;
        posB = transformB.localPosition;
        nextPos = posB;
        rbody = GetComponentInChildren<Rigidbody2D>();
        velocity = new Vector2(1.75f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        //if(movingTowards == true)
        //{
        Move();
        //}
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            movingTowards = true;
        }
    }

    private void Move()
    {
        //lift.localPosition =  Vector3.MoveTowards(lift.localPosition , nextPos , speed * Time.deltaTime);
        rbody.MovePosition(nextPos + velocity * Time.deltaTime);

        if (Vector2.Distance(lift.localPosition, nextPos) <= 0.1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        Debug.Log("Change");
        nextPos = nextPos != posA ? posA : posB;
    }
}
