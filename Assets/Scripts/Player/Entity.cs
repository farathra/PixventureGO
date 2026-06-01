using System;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;


    [Header("Player Movement Settings")]
    [SerializeField] protected float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 8;
    protected int facingDirection = 1; // 1 for right, -1 for left
    private bool facingRight = true;
    protected bool canMove = true;
    private bool canJump = true;
    private float xInput;

    [Header("Attack Details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask targetLayer;

    [Header("Ground Collision")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask ground;
    private bool isGrounded;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        anim.SetFloat("walkSpeed", 1);
    }

    protected virtual void Update()
    {
        HandleCollision();
        HandleMovement();
        HandleInput();
        // AnimJump();
        HandleAnim();
        HandleFlip();
    }


    public void GiveDamage()
    {
        Collider2D[] enemyCollider = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, targetLayer);

        foreach (Collider2D enemy in enemyCollider)
        {
            Entity entityTarget = enemy.GetComponent<Entity>();
            entityTarget.TakeDamage();
        }
    }

    private void TakeDamage()
    {
        throw new NotImplementedException();
    }

    public void EnableMoveNJump(bool enable) // This method is responsible for enabling or disabling the player's ability to move and jump, it can be called from other scripts to control the player's movement and jumping capabilities
    {
        canMove = enable;
        canJump = enable;
    }

    protected void HandleAnim() // This method handles the player's animation based on their movement
    {
        bool isMoving = rb.linearVelocity.x != 0;
        anim.SetBool("isMoving", isMoving);
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    protected virtual void HandleMovement() // This method is responsible for horizontal and vertical movement of the player
    { 
        if (canMove)
        {
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    private void Jump() // This method is responsible for making the player jump
    {
        if (isGrounded && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y);
        }
    }

    protected virtual void HandleCollision() // This method checks if the player is grounded by casting a ray downwards and checking for collisions with the ground layer
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, ground);
    }   

    private void HandleInput() // This method checks if the player has pressed the jump key (Space, W, or Up Arrow)
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.L))
        {
            HandleAttack();
        }
    }

    protected virtual void HandleAttack() // This method is responsible for making the player attack, it checks if the player is grounded and then triggers the attack animation
    {
        if (isGrounded)
        { 
            anim.SetTrigger("attack");
        }
    }

    protected void HandleFlip() // This method checks the player's horizontal velocity and flips the sprite if the player is moving in a different direction than they are currently facing
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
        facingDirection = facingDirection * -1;
    }

    private void OnDrawGizmos() // This method draws a line in the editor to visualize the ground check distance
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance, 0));
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    //private void AnimJump() // Not used, this method is responsible for setting the appropriate animation parameters based on the player's vertical velocity and grounded state
    //{
    //    if (isGrounded == false && rb.linearVelocity.y > 0)
    //    {
    //        anim.SetBool("isJumping", true);
    //        anim.SetBool("isFalling", false);
    //    }
    //    else if (isGrounded == false && rb.linearVelocity.y < 0)
    //    {
    //        anim.SetBool("isJumping", false);
    //        anim.SetBool("isFalling", true);
    //    }
    //    else if (isGrounded == true && rb.linearVelocity.y == 0)
    //    {
    //        anim.SetBool("isJumping", false);
    //        anim.SetBool("isFalling", false);
    //    }
    //}
}
