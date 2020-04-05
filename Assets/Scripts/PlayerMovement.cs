using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rBody;
    private Animator animator;
    private float jumpInput;
    private float horizontalInput;
    private bool groundCheck;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public float speed;
    public float jumpSpeed;
    public bool isJumping;
    public bool isDownSlamming;
    public float tapSpeed; //used to set howlong it takes for double pressing a key to be registered
    private float lastTapTime = 0;
    private Vector2 velocity;
    private bool jump;


    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastTapTime = 0;
        velocity = new Vector2(0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        //horizontalInput = Input.GetAxis("Horizontal");
        groundCheck = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        velocity = new Vector2(horizontalInput * speed, rBody.velocity.y);
    }


    void FixedUpdate()
    {
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }


        //transform.Translate(horizontalInput * speed * Time.deltaTime, 0, 0);
        //force = new Vector2(horizontalInput * speed, 0f);
        //rBody.AddForce(force);
        //rBody.MovePosition(rBody.position + velocity * Time.fixedDeltaTime);
        Vector3 targetVelocity = new Vector2(speed * 10f, rBody.velocity.y);


        if (jump == true && groundCheck == true)
        {
            //for some reason, translating on the y axis here did not work -- probably due to the gravity mechanism
            rBody.AddForce(new Vector2(0f, jumpSpeed));
            isJumping = true;

        }

        if (groundCheck && Input.GetButtonDown("Jump") == false)
        {
            isJumping = false;
            isDownSlamming = false;
        }

        //if (timeBtwAttack <= 0)
        //{
        //    if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.S) == false)
        //    {
        //        isAttacking = true;
        //        StartCoroutine("coRoutineAttack");
        //        //view https://www.youtube.com/watch?v=1QfxdUpVh5I
        //        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
        //        for (int i = 0; i < enemiesToDamage.Length; i++)
        //        {
        //            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
        //        }
        //        Collider2D[] objectsToDig = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsDiggable);
        //        for (int i = 0; i < objectsToDig.Length; i++)
        //        {
        //            objectsToDig[i].GetComponent<DiggingGround>().Dug();
        //        }

        //        timeBtwAttack = startTimeBtwAttack;
        //    }
        //}
        //else
        //{
        //    timeBtwAttack -= Time.deltaTime;
        //}

        if (Input.GetKeyDown(KeyCode.DownArrow) && groundCheck == false && isDownSlamming == false)//dash downwards section
        {//first detecting a double press of S key

            if ((Time.time - lastTapTime) < tapSpeed)
            {


                rBody.velocity = new Vector2(0, -15f);
                isDownSlamming = true;

            }

            lastTapTime = Time.time;

        }

        //if (isDownSlamming == true)//damage dealt to enemies when slamming down on them
        //{
        //    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(groundCheckPoint.position, groundCheckRadius, whatIsEnemy);
        //    for (int i = 0; i < enemiesToDamage.Length; i++)
        //    {
        //        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(downSlamDamage);
        //    }
        //}

        //if (Input.GetKey(KeyCode.S) && Input.GetMouseButtonDown(0))
        //{
        //    isDigging = true;
        //    Collider2D[] objectsToDig = Physics2D.OverlapCircleAll(groundCheckPoint.position, groundCheckRadius, whatIsDiggable);
        //    for (int i = 0; i < objectsToDig.Length; i++)
        //    {
        //        objectsToDig[i].GetComponent<DiggingGround>().Dug();
        //    }
        //    StartCoroutine("coRoutineDig");
        //}
    }

    //IEnumerator coRoutineAttack()
    //{
    //    yield return new WaitForSeconds(0.19f);
    //    isAttacking = false;
    //}

    //IEnumerator coRoutineDig()
    //{
    //    yield return new WaitForSeconds(0.19f);
    //    isDigging = false;
    //}


    void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
}
