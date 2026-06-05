using UnityEngine;

public class Player : Entity
{
    [Header("Player Movement Settings")]
    [SerializeField] protected float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 8;
    private float xInput;
    private bool isGrounded;
    private bool canJump = true;

    protected override void Update()
    {
        base.Update();
        HandleInput();
    }

    protected override void HandleMovement()
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

    protected override void HandleAttack()
    {
        if (isGrounded)
        {
            anim.SetTrigger("attack");
        }
    }

    protected override void HandleAnim()
    {
        base.HandleAnim();
        anim.SetBool("isGrounded", isGrounded);
    }

    protected override void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, ground);
    }

    public override void EnableMove(bool enable)
    {
        base.EnableMove(enable);
        canJump = enable;
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
}
