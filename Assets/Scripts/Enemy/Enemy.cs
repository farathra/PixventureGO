using UnityEngine;

public class Enemy : Entity
{
    private bool playerDetected;
    [Header("Player Movement Settings")]
    [SerializeField] protected float moveSpeed = 6f;

    protected override void Update()
    {
        base.Update();
        HandleAttack();
    }

    protected override void HandleAttack() // This method triggers the attack animation if the player is detected
    {
        if (playerDetected)
            anim.SetTrigger("attack");
    }

    protected override void HandleCollision() // This method checks if the player is detected by casting a circle around the attack point
    {
        base.HandleCollision();
        playerDetected = Physics2D.OverlapCircle(attackPoint.position, attackRadius, targetLayer);
    }

    protected override void HandleMovement() // This method is responsible for moving the enemy left and right based on the facing direction
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(facingDirection * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    protected override void Die()
    {
        base.Die();
        UI.instance.AddKillCount();
    }

}

