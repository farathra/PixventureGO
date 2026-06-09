using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected UI ui;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected Collider2D coll;
    protected SpriteRenderer sr;

    [Header("Health")]
    [SerializeField] protected int maxHealth = 1; 
    [SerializeField] protected int currentHealth;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private float damageDuration = 0.1f;
    private Coroutine damageFeedbackCoroutine;

    protected int facingDirection = 1; // 1 for right, -1 for left
    protected bool facingRight = true;
    protected bool canMove = true;

    [Header("Attack Details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask targetLayer;

    [Header("Ground Collision")]
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask ground;



    protected virtual void Awake()
    {
        ui = FindFirstObjectByType<UI>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim.SetFloat("walkSpeed", 1);

        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        HandleCollision();
        HandleMovement();
        // AnimJump();
        HandleAnim();
        HandleFlip();
    }


    public void GiveDamage() // This method is responsible for applying damage to the player, it calls the TakeDamage method to reduce the player's health and starts the damage feedback coroutine to change the player's material temporarily
    {
        Collider2D[] enemyCollider = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, targetLayer);

        foreach (Collider2D enemy in enemyCollider)
        {
            Entity entityTarget = enemy.GetComponent<Entity>();
            entityTarget.TakeDamage();
        }
    }

    private void TakeDamage() // This method is responsible for handling the player's health when they take damage, it reduces the player's health by 1, checks if the player has died, and starts the damage feedback coroutine to change the player's material temporarily
    {
        currentHealth = currentHealth - 1;

        PlayDamageFeedback();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void PlayDamageFeedback() // This method is responsible for starting the damage feedback coroutine, it checks if there is already a damage feedback coroutine running and stops it before starting a new one to ensure that the player's material changes correctly when taking damage multiple times in quick succession
    {
        if (damageFeedbackCoroutine != null)
        {
            StopCoroutine(damageFeedbackCoroutine);
        }

        StartCoroutine(DamageFeedbackCo());
    }

    protected virtual void Die() // This method is responsible for handling the player's death, it disables the player's movement and collision, applies a knockback effect, and destroys the player object after a short delay
    {
        anim.enabled = false;
        coll.enabled = false;

        rb.gravityScale = 8;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);

        Destroy(gameObject, 2f);
    }

    private IEnumerator DamageFeedbackCo() // This method is responsible for changing the player's material to a damage material for a short duration when they take damage, and then reverting it back to the original material
    {
        Material originalMaterial = sr.material;
        sr.material = damageMaterial;
        yield return new WaitForSeconds(damageDuration);
        sr.material = originalMaterial;
    }

    protected virtual void HandleAnim() // This method handles the player's animation based on their movement
    {
        bool isMoving = rb.linearVelocity.x != 0;
        anim.SetBool("isMoving", isMoving);
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    protected virtual void HandleMovement() // This method is responsible for horizontal and vertical movement of the player
    { 

    }

    protected virtual void HandleCollision() // This method checks if the player is grounded by casting a ray downwards and checking for collisions with the ground layer
    {
        
    }   

    protected virtual void HandleAttack() // This method is responsible for making the player attack, it checks if the player is grounded and then triggers the attack animation
    {

    }

    protected virtual void HandleFlip() // This method checks the player's horizontal velocity and flips the sprite if the player is moving in a different direction than they are currently facing
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

    public void Flip() // This method flips the player's sprite horizontally when changing direction
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDirection = facingDirection * -1;
    }

    public virtual void EnableMove(bool enable) // This method is responsible for enabling or disabling the player's ability to move and jump, it can be called from other scripts to control the player's movement and jumping capabilities
    {
        canMove = enable;
    }

    private void OnDrawGizmos() // This method draws a line in the editor to visualize the ground check distance
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance, 0));
        if (attackPoint != null )
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
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
