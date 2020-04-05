using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewPlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float runSpeed = 25f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
            Debug.Log("Jumping True");
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }

        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }


    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
        Debug.Log("Jumping False");
    }

    void FixedUpdate()

    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        
    }
}
