using UnityEngine;
using UnityEngine.Windows;

public class Enemy : Entity
{
    private bool playerDetected;

    protected override void Update()
    {
        HandleMovement();
        HandleCollision();
        HandleAnim();
        HandleFlip();
        HandleAttack();
    }

    protected override void HandleAttack()
    {
        if (playerDetected)
            anim.SetTrigger("attack");
    }

    protected override void HandleCollision()
    {
        base.HandleCollision();
        playerDetected = Physics2D.OverlapCircle(attackPoint.position, attackRadius, targetLayer);
    }

    protected override void HandleMovement()
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

}

