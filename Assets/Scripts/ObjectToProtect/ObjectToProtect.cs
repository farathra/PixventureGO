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
}
