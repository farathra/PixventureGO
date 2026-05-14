using System;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    // Player movement variables
    private float xInput;
    [Header("Player Movement Settings")]
    [SerializeField] private float jumpForce = 8;
    [SerializeField] private float moveSpeed = 3.5f;
    private Rigidbody2D rb;
    private Animator anim;
    private bool facingRight = true;

    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckDistance;
    private bool isGrounded;
    [SerializeField] private LayerMask ground;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        GroundCheck();
        XMovement();
        JumpMovement();
        // AnimJump();
        HandleAnim();
        HandleFlip();
    }


    private void HandleAnim() // This method handles the player's animation based on their movement
    {
        //bool isMoving = rb.linearVelocity.x != 0;
        //anim.SetBool("isMoving", isMoving);
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void JumpMovement() // This method checks for jump input and makes the player jump if the input is detected
    {
        if (InputUp())
        {
            Jump();
        }
    }
    private void XMovement() // This method is responsible for horizontal and vertical movement of the player
    {
        xInput = Input.GetAxisRaw("Horizontal"); 
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        
    }

    private void Jump() // This method is responsible for making the player jump
    {
        if (isGrounded == true)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void GroundCheck() // This method checks if the player is grounded by casting a ray downwards and checking for collisions with the ground layer
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, ground);
    }   

    private bool InputUp() // This method checks if the player has pressed the jump key (Space, W, or Up Arrow)
    {
        return Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.UpArrow);
    }

    private void HandleFlip() // This method checks the player's horizontal velocity and flips the sprite if the player is moving in a different direction than they are currently facing
    { 
        if (rb.linearVelocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.linearVelocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip() // This method flips the player's sprite horizontally when changing direction
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    private void OnDrawGizmos() // This method draws a line in the editor to visualize the ground check distance
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance, 0));
    }

    private void AnimJump() //Not used, this method is responsible for setting the appropriate animation parameters based on the player's vertical velocity and grounded state
    {
        if (isGrounded == false && rb.linearVelocity.y > 0)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isFalling", false);
        }
        else if (isGrounded == false && rb.linearVelocity.y < 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }
        else if (isGrounded == true && rb.linearVelocity.y == 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }
    }
}
