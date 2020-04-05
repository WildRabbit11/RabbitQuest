using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainRabbitControllerV2 : MonoBehaviour
{
    public float runSpeed; //speed of character
    public float jumpForce; //size of character's jumps
    public float dashSpeed; //speed of the character's dashes
    private float dashTime; //manages the length of the dashes
    public float startDashTime; //sets how long the dashes will be
    private int dashDirection; //sets which direction the player will dash
    private bool isDashing; //checks whether the player is dashing for animations
    private bool canDash; //bool to create a time period where the player cannot dash
    private bool quickDashReturn; //bool to allow the player to dash instantly if they were in the air
    private bool dashManager; //prevents repeat calling of coroutine
    private float horizontalMove;
    private Animator animator;
    private Rigidbody2D rbody;
    private bool isJumping;
    private Vector3  velocity = Vector3.zero;
    private bool facingRight = true;
    private bool grounded; //bool for whether character is touching the ground
    public float groundedRadius = .1f; // Radius of the overlap circle to determine if grounded
    public float playerHealth; //health of the player
    private bool takenDamage = false; //checks that enough time has passed before the player can take damage again
    private SpriteRenderer playerSprite;

    [Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character

    void Awake()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canDash = true;
        dashManager = true;
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        bool wasGrounded = grounded;
        grounded = false;

        // <Cut from fixed update>
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded)
                    Landed();
            }
        }

        //for dashing
        if(dashDirection == 0)//checks if the player is dashing or not
        { //keys currently hard coded, be sure to change later
            if (Input.GetKeyDown(KeyCode.D) && canDash == true && facingRight == false)//for moving left
            {
                dashDirection = 1;
                canDash = false;
            }
            else if (Input.GetKeyDown(KeyCode.D) && canDash == true && facingRight == true)//for moving right
            {
                dashDirection = 2;
                canDash = false;
            }
        }
        else
        {
            if(dashTime <= 0)
            {
                dashDirection = 0;
                dashTime = startDashTime;
                rbody.velocity = Vector2.zero;
                isDashing = false;
            }
            else
            {
                dashTime -= Time.deltaTime;
                isDashing = true;
                if (dashDirection == 1)
                {
                    rbody.velocity = Vector2.left * dashSpeed;
                }
                else if (dashDirection == 2)
                {
                    rbody.velocity = Vector2.right * dashSpeed;
                }
                if(grounded == false)
                {
                    quickDashReturn = true;
                }
                else
                {
                    quickDashReturn = false;
                }
            }
        }
        if (grounded == true && canDash == false && isDashing == false && dashManager == true)
        {
            dashManager = false;
            if(quickDashReturn == true)
            {
                canDash = true;
                dashManager = true;
            }
            else
            {
                StartCoroutine("coRoutineExitDash");
            }  
        }

        Move(horizontalMove * Time.fixedDeltaTime, isJumping);
        //<\Cut from fixed update>

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isDashing", isDashing);
    }

    private void Move(float move, bool jump)
    {
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, rbody.velocity.y);
        // And then smoothing it out and applying it to the character
        rbody.velocity = Vector3.SmoothDamp(rbody.velocity, targetVelocity, ref velocity, MovementSmoothing);

        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }

        // If the player should jump...
        if (grounded && jump)
        {
            // Add a vertical force to the player.
            grounded = false;
            rbody.AddForce(new Vector2(0f, jumpForce));
        }


    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void Landed() //for when the player hits the ground
    {
        isJumping = false;
    }

    IEnumerator coRoutineExitDash()
    {
        yield return new WaitForSeconds(0.3f);
        canDash = true;
        dashManager = true;
    }

    public void TakenDamage()
    {
        if (takenDamage == false)
        {
            takenDamage = true;
            playerHealth = playerHealth - 1;
            StartCoroutine("coRoutineDamageDelay");
        }
    }

    IEnumerator coRoutineDamageDelay()
    {
        playerSprite.color = new Color(1, 0, 0, 1); //turns sprite red
        rbody.velocity = new Vector2(rbody.velocity.x, 5f); //sends the player into the air
        yield return new WaitForSeconds(0.75f);//delay so that the player won't take damage again imediately
        playerSprite.color = new Color(1, 1, 1, 1); //turns sprite back to normal colour
        takenDamage = false;
        Debug.Log(takenDamage);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundedRadius);
    }
}
