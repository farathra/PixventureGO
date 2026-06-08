using UnityEngine;

public class ObjectToProtect : Entity
{
    private Transform player;

    protected override void Awake()
    {
        base.Awake();
        player = FindFirstObjectByType<Player>().transform;
    }

    protected override void Update()
    {
        HandleFlip();
    }

    protected override void HandleFlip() // This method checks the position of the player relative to the object and flips the object accordingly
    {
        if (player.transform.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (player.transform.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }

    override protected void Die() // This method is responsible for handling the object's death, it triggers the game over UI and destroys the object
    {
        base.Die();
        UI.instance.GameOver();
    }
}
