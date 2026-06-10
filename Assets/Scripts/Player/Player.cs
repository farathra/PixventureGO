using UnityEngine;

public class Player : Entity
{
    public static Player instance;

    [Header("Player Movement Settings")]
    [SerializeField] protected float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 8;
    private float xInput;
    private bool isGrounded;
    private bool canJump = true;
    private float defaultMoveSpeed;
    private float defaultJumpForce;


    protected override void Awake()
    {
        base.Awake();
        instance = this;
        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
    }

    protected override void Update()
    {
        base.Update();
        HandleInput();
        SendHealthInfo();
    }

    protected override void HandleMovement() // This method is responsible for moving the player left and right based on the horizontal input
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

    protected override void HandleAttack() // This method triggers the attack animation if the player is grounded
    {
        if (isGrounded)
        {
            anim.SetTrigger("attack");
        }
    }

    protected override void HandleAnim() // This method updates the animator parameters based on the player's state
    {
        base.HandleAnim();
        anim.SetBool("isGrounded", isGrounded);
    }

    protected override void HandleCollision() // This method checks if the player is grounded by casting a ray downwards
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, ground);
    }

    public override void EnableMove(bool enable) // This method enables or disables the player's movement and jumping ability
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

    protected override void Die()
    {
        base.Die();
        EnableMove(false);
        UI.instance.GameOver();
    }

    public void StopMoveAndJump()
    {
        moveSpeed = 0f;
        jumpForce = 0f;
    }

    public void ResetMoveAndJump()
    {
        moveSpeed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;
    }

    public int SendHealthInfo()
    {
        return currentHealth;
    }
}
