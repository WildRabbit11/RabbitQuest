using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HawkAI : MonoBehaviour
{
    public Transform target;//the player
    public float speed;
    public float nextWaypointDistance;
    public Transform enemyGFX;

    private bool chasingPlayer = false; //sets whether the bird is chasing the player or returning to the nest
    [HideInInspector] public bool flying = false; //used to control animations and to stop the bird from glitching about while at the nest
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath;

    private Seeker seeker;
    private Rigidbody2D rbody;

    private float distTarget;//distance of enemy from player
    private float distNest; //distance of enemy from nest

    private Vector3 nest;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rbody = GetComponent<Rigidbody2D>();
        nest = transform.position;

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distTarget = Vector3.Distance(target.position, transform.position);
        distNest = Vector3.Distance(nest, transform.position);
        if(distTarget >= 10f)
        {
            chasingPlayer = false;
            if(distNest <= 0.01f)
            {
                flying = false;
                rbody.velocity = new Vector3(0, 0, 0);
            }
        }

        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rbody.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        //Debug.Log(direction);
        rbody.AddForce(force);

        float distance = Vector2.Distance(rbody.position, path.vectorPath[currentWaypoint]);
        //Debug.Log(distance);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rbody.velocity.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rbody.velocity.x <= -0.01)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error) //checking that no errors have occured
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdatePath()
    {
        if (chasingPlayer == true)
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(rbody.position, target.position, OnPathComplete);
            }
        }
        else if(flying == true)
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(rbody.position, nest, OnPathComplete);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            chasingPlayer = true;
            flying = true;
        }
    }
}
